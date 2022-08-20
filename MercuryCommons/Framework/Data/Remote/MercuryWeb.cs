using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using RestSharp;
using Serilog;

namespace MercuryCommons.Framework.Data.Remote;

public static class MercuryWeb
{
    private static readonly RestClient Client = new RestClient
    {
        Options =
        {
            UserAgent = $"Mercury/{Assembly.GetExecutingAssembly().GetName().Version}",
            MaxTimeout = 3 * 1000
        }
    }.UseSerializer<JsonNetSerializer>();

    public static async Task<T> Execute<T>(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null, Dictionary<string, byte[]> files = null, Method method = Method.Get)
    {
        var request = new MercuryRequest(url, method)
        {
            OnBeforeDeserialization = resp => { resp.ContentType = "application/json; charset=utf-8"; }
        };

        if (parameters is { Count: > 0 })
        {
            foreach (var (key, value) in parameters)
            {
                request.AddParameter(key, value, ParameterType.QueryString);
            }
        }

        if (headers is { Count: > 0 })
        {
            foreach (var (key, value) in headers)
            {
                request.AddHeader(key, value.ToString() ?? string.Empty);
            }
        }

        if (files is { Count: > 0 })
        {
            foreach (var (key, value) in files)
            {
                request.AddFile(key, value, key);
            }
        }

        var response = await Client.ExecuteAsync<T>(request).ConfigureAwait(false);
        Log.Information("[{Method}] [{Status}({StatusCode})] '{Resource}'", request.Method, response.StatusDescription ?? "null", (int) response.StatusCode, response.ResponseUri?.OriginalString ?? "null");
        return response.Data;
    }

    public static async Task DownloadFileAsync(string fileLink, string path)
    {
        var data = GetByteArray(fileLink);
        await File.WriteAllBytesAsync(path, data);
    }

    public static byte[] GetByteArray(string url)
    {
        var request = new RestRequest(url);
        var data = Client.Execute(request);
        byte[] retData = null;
        if (data.IsSuccessStatusCode) retData = Client.DownloadData(request);
        return retData;
    }
}