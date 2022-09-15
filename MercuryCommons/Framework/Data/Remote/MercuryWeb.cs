using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using RestSharp;
using Serilog;

namespace MercuryCommons.Framework.Data.Remote;

/// <summary>
/// Central entrypoint for all web requests through Mercury
/// </summary>
public class MercuryWeb
{
    /// <summary>
    /// Main web client for making requests through
    /// </summary>
    private static readonly RestClient Client = new RestClient
    {
        Options =
        {
            UserAgent = $"Mercury/{Assembly.GetExecutingAssembly().GetName().Version}",
            MaxTimeout = 3 * 1000
        }
    }.UseSerializer<JsonNetSerializer>();

    /// <summary>
    /// Execute request expecting a json response
    /// </summary>
    /// <param name="url">Url to request from</param>
    /// <param name="parameters">Query parameters to include in the request</param>
    /// <param name="headers">Headers to include in the request</param>
    /// <param name="files">Files to include in the request</param>
    /// <param name="method">HTTP method to use</param>
    /// <typeparam name="T">JSON model to use in return value</typeparam>
    /// <returns>JSON model created from json</returns>
    public static async Task<T> Execute<T>(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null, Dictionary<string, byte[]> files = null, object body = null, Method method = Method.Get)
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

        if (body != null) request.AddJsonBody(body);
        
        var response = await Client.ExecuteAsync<T>(request).ConfigureAwait(false);
        Log.Information("[{Method}] [{Status}({StatusCode})] '{Resource}'", request.Method, response.StatusDescription ?? "null", (int) response.StatusCode, response.ResponseUri?.OriginalString ?? "null");
        return response.Data;
    }

    /// <summary>
    /// Download a url to a file. Should only be used with urls where the filetype is known.
    /// </summary>
    /// <param name="url">Url of site</param>
    /// <param name="path">Local file path to download to, including extension</param>
    public static async Task DownloadFileAsync(string url, string path)
    {
        var data = GetByteArray(url);
        await File.WriteAllBytesAsync(path, data);
    }

    /// <summary>
    /// Get the raw bytes of a URL, such as an image or binary data.
    /// </summary>
    /// <param name="url">Url of site</param>
    /// <returns>Data of site, null if not successful</returns>
    public static byte[] GetByteArray(string url)
    {
        var request = new RestRequest(url);
        var data = Client.Execute(request);
        byte[] retData = null;
        if (data.IsSuccessful) retData = Client.DownloadData(request);
        return retData;
    }
}