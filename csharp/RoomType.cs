namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the RoomType type in schema/lakbay.graphql. A real "pick your
/// room" option within an Accommodation — see the 2026-09-08 Inghams
/// research (Hotel Post, St Anton: "St Anton room," "Galzig room,"
/// "Single room," each its own size/bed configuration/occupancy/price).
/// Synced as its own top-level entity, not embedded on Accommodation —
/// same "queried separately by parent id" shape ProductFilter.destinationId
/// already uses for Product/Destination, not a new pattern.
/// </summary>
public sealed record RoomType
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    /// <summary>Descriptive range, e.g. "18-22m²" — matches real hotel copy, not a precise measurement.</summary>
    public string? SizeSqm { get; init; }

    public required string BedConfiguration { get; init; }
    public required int MaxOccupancy { get; init; }
    public required BoardBasis BoardBasis { get; init; }
    public required IReadOnlyList<PriceBand> PriceBands { get; init; }

    /// <summary>Nullable — falls back to the owning Accommodation's own photo in the UI rather than a fabricated distinct-room photo (see ADR-0018's photo-honesty rule, extended one level deeper).</summary>
    public string? HeroImageUrl { get; init; }

    /// <summary>Nullable — only set where a longer stay is realistic (ADR-0020). A discounted flat monthly price, not a date-ranged PriceBand; real booking of an open-ended stay is still Phase 4 work.</summary>
    public decimal? MonthlyRatePhp { get; init; }

    /// <summary>Plain reference to the owning Accommodation — see the type-level note on why this isn't an embedded object.</summary>
    public required string AccommodationId { get; init; }

    public required DateTime SourceUpdatedUtc { get; init; }
}
