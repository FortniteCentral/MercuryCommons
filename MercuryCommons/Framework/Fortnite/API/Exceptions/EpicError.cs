using System.Diagnostics;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Exceptions;

[DebuggerDisplay("{" + nameof(ErrorMessage) + "}")]
public class EpicError
{
    [J] public string ErrorCode { get; set; }
    [J] public string ErrorMessage { get; set; }
    [J] public object[] MessageVars { get; set; }
    [J] public int NumericErrorCode { get; set; }
    [J] public string OriginatingService { get; set; }
    [J] public string Intent { get; set; }
    [J] public string ErrorDescription { get; set; }
    [J] public string Error { get; set; }
}