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
    /// The specific place guests stay — Lakbay sells a holiday, not just
    /// a destination (see the 2026-09-08 Hotelplan-pattern research:
    /// Inghams/Inntravel/Santa's Lapland all sell a named stay, not a
    /// bare place). Promoted from two flat strings to a real, reusable
    /// entity in ADR-0017 — the "revisit only if..." case ADR-0016
    /// explicitly flagged. Nullable, not required: legacy pre-restructure
    /// products (if any survive a boot without a reseed) keep compiling.
    /// </summary>
    public Accommodation? Accommodation { get; init; }

    /// <summary>Bundled into PriceBands — no separate charge. Defaults to empty, not required, for the same pre-existing-data reason as AccommodationName above.</summary>
    public IReadOnlyList<string> IncludedActivities { get; init; } = [];

    /// <summary>Bookable add-ons on top of the package — never priced by this field; a real per-activity price is a future concern if this proves worth it, not designed in speculatively now.</summary>
    public IReadOnlyList<string> OptionalActivities { get; init; } = [];

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
