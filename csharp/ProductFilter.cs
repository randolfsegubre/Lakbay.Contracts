namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the ProductFilter input type in schema/lakbay.graphql.
/// </summary>
public sealed record ProductFilter
{
    public ProductLineCode? ProductLine { get; init; }
    public string? DestinationId { get; init; }
    public decimal? MinPricePhp { get; init; }
    public decimal? MaxPricePhp { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool? ExcludeSoldOut { get; init; }
}
