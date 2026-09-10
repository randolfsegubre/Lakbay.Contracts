namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the ProductFilter input type in schema/lakbay.graphql - every
/// field optional, so a caller only constrains what it actually cares
/// about (an empty/all-null filter means "everything"). Consumed by
/// Lakbay.AvailabilityApi's Query.products resolver, which translates each
/// set field into its own MongoDB filter clause, ANDed together.
/// </summary>
public sealed record ProductFilter
{
    public ProductLineCode? ProductLine { get; init; }
    /// <summary>Deliberately String, not ID - see ADR-0019: HotChocolate only infers GraphQL ID for a member literally named "Id".</summary>
    public string? DestinationId { get; init; }
    public decimal? MinPricePhp { get; init; }
    public decimal? MaxPricePhp { get; init; }
    /// <summary>Matches products with at least one PriceBand overlapping [StartDate, EndDate] - not an exact-match date.</summary>
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    /// <summary>When true, excludes products with AvailableCount == 0 (ADR-0011's atomic-decrement counter).</summary>
    public bool? ExcludeSoldOut { get; init; }
}
