using MercuryCommons.Framework.Fortnite.API.Objects.Catalog;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.GraphQL;

public class GQLCatalogRelatedResponse
{
    [J] public CatalogElement[] Elements { get; set; }
    [J] public Paging Paging { get; set; }
}

public class CatalogElement
{
    [J] public string Id { get; set; }
    [J] public CustomAttribute[] CustomAttributes { get; set; }
}

public class Paging
{
    [J] public int Total { get; set; }
}