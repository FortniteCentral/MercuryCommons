using System;
using System.Text;

namespace MercuryCommons.Framework.Fortnite.API;

/// <summary>
/// Every application that utilises Epic's official services is issues an auth client
/// that is used to identify requests sent to epic, as well as to set boundaries to
/// what a certain application can access.
/// </summary>
public class ClientToken
{
    public static ClientToken FortnitePcGameClient => CreateToken("ec684b8c687f479fadea3cb2ad83f5c6", "e1f31c211f28413186262d37a13fc84d");
    public static ClientToken FortniteIosGameClient => CreateToken("3446cd72694c4a4485d81b77adbb2141", "9209d4a5e25a457fb9b07489d313b41a");
    public static ClientToken FortniteAndroidGameClient => CreateToken("3f69e56c7649492c8cc29f1af08a8a12", "b51ee9cb12234f50a69efa67ef53812e");
    public static ClientToken FortniteValkyrieGameClient => CreateToken("3e13c5c57f594a578abe516eecb673fe", "530e316c337e409893c55ec44f22cd62");
    
    public static ClientToken WexIosGameClient = CreateToken("ec813099a59f48d4a338f1901c1609db", "72f6db62-0e3e-4439-97df-ee21f7b0ae94");
    
    public static ClientToken ProdFn => CreateToken("xyza7891343Fr4ZSPkQZ3kaL3I2sX8B5", "F8BVRyHIqmct8cN9KSPbXsJszpiIZEYEFDiySxc1wuA");
    public static ClientToken LauncherAppClient2 => CreateToken("34a02cf8f4414e29b15921876da36f9a", "daafbccc737745039dffe53d94fc76cf");
    public static ClientToken DauntlessDiesel => CreateToken("b070f20729f84693b5d621c904fc5bc2", "HG@XE&TGCxEJsgT#&_p2]=aRo#~>=>+c6PhR)zXP");

    /// <summary>
    /// The id of the client.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Secret
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Base64 of the client token, encoded as <see cref="ClientId"/>:<see cref="Secret"/>
    /// </summary>
    public string Base64 { get; set; }

    /// <summary>
    /// Creates a client token with a client id and secret, they are encoded in base64 as <see cref="ClientId"/>:<see cref="Secret"/>
    /// </summary>
    /// <param name="clientId">The id of the client</param>
    /// <param name="secret">The secret</param>
    /// <returns>Client token</returns>
    public static ClientToken CreateToken(string clientId, string secret)
    {
        return new ClientToken
        {
            ClientId = clientId,
            Secret = secret,
            Base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{secret}"))
        };
    }
}