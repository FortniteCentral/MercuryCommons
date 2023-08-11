using System;

namespace MercuryCommons.Framework.Fortnite.API.Exceptions;

public class FortniteException(string message) : Exception(message)
{
    public EpicError EpicError { get; set; }

    public FortniteException(string message, EpicError epicError) : this($"{message} Epic Message: {epicError.ErrorMessage}")
    {
        EpicError = epicError;
    }
}