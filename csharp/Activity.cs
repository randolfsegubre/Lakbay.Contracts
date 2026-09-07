namespace Lakbay.Contracts;

/// <summary>
/// A real, independently bookable local activity at a fixed, agreed-upfront
/// price (ADR-0020) — Lakbay's answer to "buying directly from a local
/// risks getting scammed or overpriced." Modeled on the real Klook/
/// GetYourGuide pattern confirmed via research: a fixed price and every
/// inclusion itemized in writing, not named after a tourist is already
/// committed. Embeds a full <see cref="Destination"/> (like Product), not
/// a thin id reference (unlike RoomType) — Activities are independently
/// cross-destination browsable, so they need the richer object the same
/// way Accommodation now does.
/// </summary>
public sealed record Activity
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    /// <summary>Descriptive, e.g. "Half-day (4 hours)" — same pragmatism as RoomType.SizeSqm, not a precise ISO duration.</summary>
    public required string DurationLabel { get; init; }

    /// <summary>Fixed, per-person, agreed upfront — the whole point.</summary>
    public required decimal PricePhp { get; init; }

    /// <summary>Itemized inclusions (guide, gear, fees, meals) — "get every inclusion in writing" is the real anti-scam practice this mirrors.</summary>
    public IReadOnlyList<string> Includes { get; init; } = [];

    public string? HeroImageUrl { get; init; }
    public required Destination Destination { get; init; }
    public required DateTime SourceUpdatedUtc { get; init; }
}
