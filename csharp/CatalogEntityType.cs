namespace Lakbay.Contracts;

/// <summary>Which catalog type a <see cref="CatalogSyncEvent"/> carries.</summary>
public enum CatalogEntityType
{
    ProductLine,
    Destination,
    Product,
    Country,
    Region,
    Accommodation,
    RoomType,
    Activity,
}
