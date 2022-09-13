using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MercuryCommons.Framework.Unreal.Manifests.Objects;

public class ManifestInfo
{
    public string AppName { get; }
    public string LabelName { get; }
    public string BuildVersion { get; set; }
    public Version Version { get; }
    public int CL { get; }
    public string Hash { get; }
    public List<UriBuilder> Uris { get; } = new();
    public string FileName { get; }

    public ManifestInfo() { }
    public ManifestInfo(Stream jsonStream, int idx = 0) : this(JsonDocument.Parse(jsonStream), idx) { }
    public ManifestInfo(byte[] jsonBytes, int idx = 0) : this(JsonDocument.Parse(jsonBytes), idx) { }
    public ManifestInfo(string jsonString, int idx = 0) : this(JsonDocument.Parse(jsonString), idx) { }

    public ManifestInfo(JsonDocument jsonDocument, int idx = 0)
    {
        var rootElement = jsonDocument.RootElement.GetProperty("elements")[idx];
        AppName = rootElement.GetProperty("appName").GetString();
        LabelName = rootElement.GetProperty("labelName").GetString();
        BuildVersion = rootElement.GetProperty("buildVersion").GetString();
        Hash = rootElement.GetProperty("hash").GetString();

        var buildMatch = Regex.Match(BuildVersion, @"(\d+(?:\.\d+)+)-CL-(\d+)", RegexOptions.Singleline);

        if (buildMatch.Success)
        {
            Version = Version.Parse(buildMatch.Groups[1].Value);
            CL = int.Parse(buildMatch.Groups[2].Value);
        }

        var manifestsArray = rootElement.GetProperty("manifests");
        var manifestUriBuilders = new List<UriBuilder>();

        foreach (var manifestElement in manifestsArray.EnumerateArray())
        {
            var uriBuilder = new UriBuilder(manifestElement.GetProperty("uri").GetString());

            if (manifestElement.TryGetProperty("queryParams", out var queryParamsArray))
            {
                foreach (var query in from queryParam in queryParamsArray.EnumerateArray()
                         let queryParamName = queryParam.GetProperty("name").GetString()
                         let queryParamValue = queryParam.GetProperty("value").GetString()
                         select $"{queryParamName}={queryParamValue}")
                {
                    if (uriBuilder.Query.Length == 0)
                    {
                        uriBuilder.Query = query;
                    }
                    else
                    {
                        uriBuilder.Query += '&' + query;
                    }
                }
            }

            manifestUriBuilders.Add(uriBuilder);
        }

        Uris.AddRange(manifestUriBuilders);
        FileName = Uris.FirstOrDefault()?.Uri.Segments[^1];
    }

    public byte[] DownloadManifestData(string cacheDir = null)
    {
        var path = cacheDir == null ? null : Path.Combine(cacheDir, FileName);

        if (path != null && File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }

        using var client = new HttpClient();
        byte[] data = null;

        foreach (var uri in Uris.TakeWhile(_ => data == null))
        {
            try
            {
                data = client.GetByteArrayAsync(uri.Uri).GetAwaiter().GetResult();
            }
            catch
            {
                data = null;
            }
        }

        if (path != null && data != null)
        {
            File.WriteAllBytes(path, data);
        }

        return data;
    }

    public string DownloadManifestString(string cacheDir = null)
    {
        return Encoding.UTF8.GetString(DownloadManifestData(cacheDir));
    }

    public async Task<byte[]> DownloadManifestDataAsync(string cacheDir = null)
    {
        var path = cacheDir == null ? null : Path.Combine(cacheDir, FileName);

        if (path != null && File.Exists(path))
        {
            return await File.ReadAllBytesAsync(path).ConfigureAwait(false);
        }

        using var client = new HttpClient();
        byte[] data = null;

        foreach (var uri in Uris.TakeWhile(_ => data == null))
        {
            try
            {
                data = await client.GetByteArrayAsync(uri.Uri).ConfigureAwait(false);
            }
            catch
            {
                data = null;
            }
        }

        if (path != null && data != null)
        {
            await File.WriteAllBytesAsync(path, data).ConfigureAwait(false);
        }

        return data;
    }

    public async Task<string> DownloadManifestStringAsync(string cacheDir = null)
    {
        var data = await DownloadManifestDataAsync(cacheDir).ConfigureAwait(false);
        return Encoding.UTF8.GetString(data);
    }
}