namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the Product type in schema/lakbay.graphql.
/// </summary>
public sealed record Product
{
    public required string Id { get; init; }
    public required string Slug { get; init; }
    public required string Name { get; init; }
    public required ProductLineCode ProductLine { get; init; }
    public required Destination Destination { get; init; }
    public required string Summary { get; init; }
    public required int ItineraryDays { get; init; }
    public required BoardBasis BoardBasis { get; init; }
    public required IReadOnlyList<PriceBand> PriceBands { get; init; }

    /// <summary>
    /// Current bookable count. Lakbay.Booking is the only writer of the
    /// real value (ADR-0011, an atomic conditional decrement). This
    /// field on Lakbay.AvailabilityApi's copy is a read-model mirror kept
    /// current via AvailabilityChanged events (ADR-0008), never a second
    /// source of truth.
    /// </summary>
    public required int AvailableCount { get; init; }

    public bool IsSoldOut => AvailableCount <= 0;

    public string? HeroImageUrl { get; init; }
    public required DateTime SourceUpdatedUtc { get; init; }
}
