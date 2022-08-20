using System;

namespace MercuryCommons.Framework.Fortnite.API.Exceptions;

public class FortniteException : Exception
{
    public EpicError EpicError { get; set; }

    public FortniteException(string message) : base(message) { }

    public FortniteException(string message, EpicError epicError) : base($"{message} Epic Message: {epicError.ErrorMessage}")
    {
        EpicError = epicError;
    }
}