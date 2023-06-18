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
    private static readonly RestClient Client = new(new RestClientOptions
    {
        UserAgent = $"Mercury/{Assembly.GetExecutingAssembly().GetName().Version}",
        MaxTimeout = 3 * 1000
    }, configureSerialization: s => s.UseSerializer<JsonNetSerializer>());

    /// <summary>
    /// Execute request expecting a json response
    /// </summary>
    /// <param name="url">Url to request from</param>
    /// <param name="parameters">Query parameters to include in the request</param>
    /// <param name="headers">Headers to include in the request</param>
    /// <param name="files">Files to include in the request</param>
    /// <param name="body">Json body to include in the request</param>
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
        Log.Information("[{Method}] [{Status}({StatusCode})] '{Resource}'", request.Method, response.StatusDescription ?? "null", (int) response.StatusCode, request.Resource);
        return response.Data;
    }

    /// <summary>
    /// Execute request to get a full response
    /// </summary>
    /// <param name="url">Url to request from</param>
    /// <param name="parameters">Query parameters to include in the request</param>
    /// <param name="headers">Headers to include in the request</param>
    /// <param name="files">Files to include in the request</param>
    /// <param name="body">Json body to include in the request</param>
    /// <param name="method">HTTP method to use</param>
    /// <returns>RestResponse of request</returns>
    public static async Task<RestResponse> ExecuteForResponse(string url, Dictionary<string, object> parameters = null, Dictionary<string, object> headers = null, Dictionary<string, byte[]> files = null, object body = null, Method method = Method.Get)
    {
        var request = new MercuryRequest(url, method);

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

        return await ExecuteForResponse(request, Client);
    }

    public static async Task<RestResponse> ExecuteForResponse(MercuryRequest request, RestClient client = null)
    {
        client ??= Client;
        var response = await client.ExecuteAsync(request).ConfigureAwait(false);
        Log.Information("[{Method}] [{Status}({StatusCode})] '{Resource}'", request.Method, response.StatusDescription ?? "null", (int) response.StatusCode, request.Resource);
        return response;
    }

    /// <summary>
    /// Download a url to a file. Should only be used with urls where the filetype is known.
    /// </summary>
    /// <param name="url">Url of site</param>
    /// <param name="path">Local file path to download to, including extension</param>
    public static async Task DownloadFileAsync(string url, string path)
    {
        var stream = await GetStreamAsync(url);
        await using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
        await stream.CopyToAsync(fileStream);
    }

    /// <summary>
    /// Get a stream of data from a URL, such as an image or binary data.
    /// </summary>
    /// <param name="url">Url of site</param>
    /// <returns>Data stream of site data</returns>
    public static async Task<Stream> GetStreamAsync(string url)
    {
        var request = new MercuryRequest(url);
        return await Client.DownloadStreamAsync(request);
    }

    /// <summary>
    /// Get the raw bytes of a URL, such as an image or binary data.
    /// </summary>
    /// <param name="url">Url of site</param>
    /// <returns>Data of site, null if not successful</returns>
    public static byte[] GetByteArray(string url)
    {
        var request = new MercuryRequest(url);
        return GetByteArray(request);
    }

    /// <summary>
    /// Get the raw bytes of a URL asynchronously, such as an image or binary data.
    /// </summary>
    /// <param name="url">Url of site</param>
    /// <returns>Data of site, null if not successful</returns>
    public static async Task<byte[]> GetByteArrayAsync(string url)
    {
        var request = new MercuryRequest(url);
        var data = await Client.ExecuteAsync(request);
        byte[] retData = null;
        if (data.IsSuccessful) retData = data.RawBytes;
        return retData;
    }

    /// <summary>
    /// Get the raw bytes of a request, such as an image or binary data.
    /// </summary>
    /// <param name="request">Created request class</param>
    /// <returns>Data of site, null if not successful</returns>
    public static byte[] GetByteArray(MercuryRequest request)
    {
        var data = Client.Execute(request);
        byte[] retData = null;
        if (data.IsSuccessful) retData = data.RawBytes;
        return retData;
    }
}