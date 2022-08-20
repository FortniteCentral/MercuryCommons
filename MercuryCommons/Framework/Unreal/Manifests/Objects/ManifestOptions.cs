using System;
using System.IO;

namespace MercuryCommons.Framework.Unreal.Manifests.Objects;

public class ManifestOptions
{
    public Uri ChunkBaseUri { get; set; }
    public DirectoryInfo ChunkCacheDirectory { get; set; }
}