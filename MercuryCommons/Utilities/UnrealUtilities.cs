using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CUE4Parse_Conversion.Textures;
using CUE4Parse.FileProvider.Objects;
using CUE4Parse.FileProvider.Vfs;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.VirtualFileSystem;
using MercuryCommons.Framework.Unreal;
using Newtonsoft.Json.Linq;
using SkiaSharp;

namespace MercuryCommons.Utilities;

public static class UnrealUtilities
{
    public static CustomFileProvider Provider = null; // Must be initialized before usage

    public static string GetVersion(dynamic buildString)
    {
        var usedVersion = buildString switch
        {
            JValue { Type: JTokenType.String } val => val.ToString(CultureInfo.InvariantCulture),
            string val => val,
            object val => val.ToString() ?? string.Empty, // generic
            _ => string.Empty
        };

        var buildMatch = Regex.Match(usedVersion, @"Fortnite\+Release\-([0-9]+\.[0-9]+)\-");
        if (buildMatch.Success) return $"{float.Parse(buildMatch.Groups[1].Value):0.00}";
        buildMatch = Regex.Match(usedVersion, @"Fortnite\+Release\-([0-9]+\.[0-9]+\.[0-9]+)\-");
        return buildMatch.Success ? buildMatch.Groups[1].Value : string.Empty;
    }

    public static IList<string> GetPartialKeys(this IAesVfsReader provider, string partialKey) =>
        provider.Files.Values.GetMatchFromGameFile(partialKey) is { Count: > 0 } files ? files : new List<string>();
    public static IList<string> GetPartialKeys(this IVfsFileProvider provider, string partialKey) =>
        provider.Files.Values.GetMatchFromGameFile(partialKey) is { Count: > 0 } files ? files : new List<string>();
    public static IList<string> GetMatchFromGameFile(this IEnumerable<GameFile> enumerable, string pattern)
        => (from file in enumerable where Regex.IsMatch(file.Path, pattern, RegexOptions.IgnoreCase) select file.Path).ToList();

    public static string GetPakNumber(this IAesVfsReader provider) => Regex.Match(provider.Name, @"\d+").Value;
    public static IList<string> ExportsToName(this IEnumerable<UObject> exports) => exports.Select(export => export.ExportType).ToList();

    public static bool TryLoadObject<T>(FSoftObjectPath path, out T export) where T : UObject => TryLoadObject(path.AssetPathName.Text, out export);
    public static bool TryLoadObject<T>(string fullPath, out T export) where T : UObject => Provider.TryLoadObject(fullPath.Replace(".uasset", string.Empty), out export);

    public static SKBitmap GetBitmap(FSoftObjectPath? softObjectPath) => GetBitmap(softObjectPath?.AssetPathName.Text);
    public static SKBitmap GetBitmap(string fullPath) => TryLoadObject(fullPath, out UTexture2D texture) ? GetBitmap(texture) : null;
    public static SKBitmap GetBitmap(UTexture2D texture)
    {
        if (texture is null || texture.IsVirtual) return null;
        return texture.Decode();
    }
    public static SKBitmap GetBitmap(FPackageIndex packageIndex)
    {
        while (true)
        {
            if (!packageIndex.TryLoad(out var export) || export == null) return null;
            if (export is UTexture2D texture) return GetBitmap(texture);
        }
    }

    public static SKBitmap ResizeWithRatio(this SKBitmap me, double width, double height)
    {
        var ratioX = width / me.Width;
        var ratioY = height / me.Height;
        var ratio = ratioX < ratioY ? ratioX : ratioY;
        return me.Resize(Convert.ToInt32(me.Width * ratio), Convert.ToInt32(me.Height * ratio));
    }

    public static SKBitmap Resize(this SKBitmap me, int size) => me.Resize(size, size);
    public static SKBitmap Resize(this SKBitmap me, int width, int height)
    {
        var bmp = new SKBitmap(new SKImageInfo(width, height), SKBitmapAllocFlags.ZeroPixels);
        using var pixmap = bmp.PeekPixels();
        me.ScalePixels(pixmap, SKFilterQuality.Medium);
        return bmp;
    }
}