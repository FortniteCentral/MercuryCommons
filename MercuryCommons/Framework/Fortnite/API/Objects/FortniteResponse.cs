﻿using System.Net;
using MercuryCommons.Framework.Fortnite.API.Exceptions;
using Newtonsoft.Json;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects;

public class FortniteResponse
{
    [J] public EpicError Error { get; set; }
    [J] public HttpStatusCode HttpStatusCode { get; set; }

    [JsonIgnore] public bool IsSuccessful => Error == null;
}

public class FortniteResponse<T> : FortniteResponse
{
    [J] public T Data { get; set; }
}