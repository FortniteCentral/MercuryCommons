﻿using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;
using SkiaSharp;

namespace MercuryCommons.Utilities;

public static class UnrealUtilities
{
    public static AbstractVfsFileProvider Provider = null; // Must be initialized before usage

    public static string GetVersion(dynamic buildString)
    {
        var usedVersion = buildString switch
        {
            JValue { Type: JTokenType.String } val => val.ToString(CultureInfo.InvariantCulture),
            string val => val,
            object val => val.ToString() ?? string.Empty, // generic
            _ => string.Empty
        };

        var buildMatch = Regex.Match(usedVersion, @"Fortnite\+Release-([0-9]+\.[0-9]+)-");
        if (buildMatch.Success) return $"{float.Parse(buildMatch.Groups[1].Value):0.00}";
        buildMatch = Regex.Match(usedVersion, @"Fortnite\+Release-([0-9]+\.[0-9]+\.[0-9]+)-");
        return buildMatch.Success ? buildMatch.Groups[1].Value : string.Empty;
    }

    public static List<string> GetPartialKeys(this IAesVfsReader provider, string partialKey) =>
        provider.Files.Values.GetMatchFromGameFile(partialKey) is { Count: > 0 } files ? files : new List<string>();
    public static List<string> GetPartialKeys(this IVfsFileProvider provider, string partialKey) =>
        provider.Files.Values.GetMatchFromGameFile(partialKey) is { Count: > 0 } files ? files : new List<string>();
    public static List<string> GetMatchFromGameFile(this IEnumerable<GameFile> enumerable, string pattern)
        => (from file in enumerable where Regex.IsMatch(file.Path, pattern, RegexOptions.IgnoreCase) select file.Path).ToList();
    public static List<string> GetPartialKeys(this IVfsFileProvider provider, Regex regex) =>
        provider.Files.Values.GetMatchFromGameFile(regex) is { Count: > 0 } files ? files : new List<string>();
    public static List<string> GetMatchFromGameFile(this IEnumerable<GameFile> enumerable, Regex regex)
        => (from file in enumerable where regex.IsMatch(file.Path) select file.Path).ToList();

    public static string GetPakNumber(this IAesVfsReader provider) => Regex.Match(provider.Name, @"\d+").Value;
    public static IList<string> ExportsToName(this IEnumerable<UObject> exports) => exports.Select(export => export.ExportType).ToList();

    public static bool TryLoadObject<T>(FSoftObjectPath path, out T export) where T : UObject => TryLoadObject(path.AssetPathName.Text, out export);
    public static bool TryLoadObject<T>(string fullPath, out T export) where T : UObject => Provider.TryLoadPackageObject(fullPath.Replace(".uasset", string.Empty), out export);
}