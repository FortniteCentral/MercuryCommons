using System;
using System.Collections.Generic;
using System.IO;
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
    public string BuildVersion { get; }
    public Version Version { get; }
    public int CL { get; }
    public string Hash { get; }
    public Uri Uri { get; }
    public string FileName { get; }

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
                foreach (var queryParam in queryParamsArray.EnumerateArray())
                {
                    var queryParamName = queryParam.GetProperty("name").GetString();
                    var queryParamValue = queryParam.GetProperty("value").GetString();
                    var query = $"{queryParamName}={queryParamValue}";

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

        Uri = (manifestUriBuilders.Find(x => x.Query.Length == 0) ?? manifestUriBuilders[0]).Uri;
        FileName = Uri.Segments[^1];
    }

    public byte[] DownloadManifestData(string cacheDir = null)
    {
        var path = cacheDir == null ? null : Path.Combine(cacheDir, FileName);

        if (path != null && File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }

        using var wc = new HttpClient();
        var data = wc.GetByteArrayAsync(Uri).GetAwaiter().GetResult();

        if (path != null)
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
        var data = await client.GetByteArrayAsync(Uri).ConfigureAwait(false);

        if (path != null)
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