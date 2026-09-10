namespace Lakbay.Contracts;

/// <summary>
/// A date-ranged price for a Product or RoomType (e.g. "Peak Season,"
/// "Off-Peak") - a Product/RoomType carries a list of these rather than one
/// flat price, since real travel pricing varies by season. Mirrors the
/// PriceBand type in schema/lakbay.graphql; stored as a JSON array in
/// Lakbay.Cms (see that repo's CatalogContentTypeSeeder) since it isn't a
/// standalone Umbraco content node.
/// </summary>
public sealed record PriceBand
{
    public required string Label { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required decimal PricePhp { get; init; }
}
