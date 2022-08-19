using System;
using System.Linq;
using System.Security.Cryptography;

namespace MercuryCommons.Utilities.Extensions;

public static class StringExtensions
{
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