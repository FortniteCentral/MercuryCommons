namespace MercuryCommons.Framework.Fortnite.API.Enums;

public enum EpicPlatform
{
    Windows,
    Mac,
    IOS,
    Android
}

public static class PlatformExtension
{
    public static string GetForwardFacingName(this EpicPlatform platform)
    {
        return platform switch
        {
            EpicPlatform.Windows => "Windows",
            EpicPlatform.Mac => "MacOS",
            EpicPlatform.IOS => "iOS",
            EpicPlatform.Android => "Android",
            _ => "Windows"
        };
    }
}