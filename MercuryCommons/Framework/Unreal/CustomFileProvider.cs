using System;
using System.IO;
using CUE4Parse.FileProvider.Vfs;
using CUE4Parse.UE4.Readers;
using CUE4Parse.UE4.Versions;

namespace MercuryCommons.Framework.Unreal;

public abstract class CustomFileProvider : AbstractVfsFileProvider // Used as a type base for other custom file provider implementations
{
    public CustomFileProvider(bool isCaseInsensitive = false, VersionContainer versions = null) : base(isCaseInsensitive, versions) { }

    public void Initialize(Func<string, Stream[], Func<string, FArchive>> initializeFunction)
    {
        
    }
}