using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MercuryCommons.Utilities.Extensions;

public static class StringExtensions
{
    public static string GetSHA1Hash(this string text)
    {
        using var cryptoProvider = SHA1.Create();
        return BitConverter.ToString(cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty).ToLower();
    }

    public static string GetSHA1Hash(this byte[] bytes)
    {
        using var cryptoProvider = SHA1.Create();
        return BitConverter.ToString(cryptoProvider.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
    }

    public static string FirstCharToUpper(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input[1..]
        };
    }

    public static string FirstCharToLower(this string input)
    {
        return input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToLower() + input[1..]
        };
    }

    public static void NotNullOrEmpty(this string str, string name)
    {
        if (string.IsNullOrEmpty(str)) throw new ArgumentException($"{name} can not be null or empty.", name);
    }

    public static void NotNull(this object obj, string name)
    {
        if (obj == null) throw new ArgumentException($"{name} can not be null.", name);
    }

    public static string EncodeBase64(this string input) => Convert.ToBase64String(input.ToHexByteArray());

    public static string DecodeBase64(this string input)
    {
        var bytes = Convert.FromBase64String(input);
        var hex = BitConverter.ToString(bytes);
        return hex.Replace("-", "");
    }

    public static byte[] ToHexByteArray(this string inputHex)
    {
        var resultantArray = new byte[inputHex.Length / 2];
        for (var i = 0; i < resultantArray.Length; i++)
        {
            resultantArray[i] = Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
        }

        return resultantArray;
    }
}