using System.IO;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Exceptions;
using Newtonsoft.Json;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Auth;

public class Device(string deviceId, string accountId, string secret)
{
    [J] public string DeviceId { get; set; } = deviceId;
    [J] public string AccountId { get; set; } = accountId;
    [J] public string Secret { get; set; } = secret;
    [J] public string UserAgent { get; set; }
    [J] public DeviceLocation Created { get; set; }
    [J] public DeviceLocation LastAccess { get; set; }

    public async Task SaveToFileAsync(string path, bool deleteIfExists = true)
    {
        if (!deleteIfExists && File.Exists(path))
        {
            throw new FortniteException("A file already exists with that name.");
        }

        if (string.IsNullOrEmpty(DeviceId) || string.IsNullOrEmpty(Secret))
        {
            throw new FortniteException("Tried to save device to file but device was null.");
        }

        var content = JsonConvert.SerializeObject(this);
        if (File.Exists(path) && deleteIfExists) File.Delete(path);
        if (!string.IsNullOrEmpty(path)) await File.WriteAllTextAsync(path, content);
    }
}