namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the Accommodation type in schema/lakbay.graphql. Promoted off
/// Product's flat accommodationName/accommodationDescription strings
/// (ADR-0016) into a real, reusable content entity with its own
/// highlights — the "revisit only if..." case ADR-0016 explicitly
/// flagged, now that the geography restructure (ADR-0017) called for it.
/// </summary>
public sealed record Accommodation
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public IReadOnlyList<string> Highlights { get; init; } = [];
    public string? HeroImageUrl { get; init; }

    /// <summary>
    /// Which Destination this stay is in — was only reachable indirectly
    /// via Product before (a Product picked both a Destination and an
    /// Accommodation as siblings). Added so Accommodation can be browsed
    /// and searched on its own (the new Stays page), matching the
    /// 2026-09-08 "search for a hotel first" scenario. Required, like
    /// Product.Destination — the established stale-shape migration
    /// (delete-and-recreate) means every Accommodation is refreshed with
    /// a real picked Destination before this ships, never a partial one.
    /// </summary>
    public required Destination Destination { get; init; }

    /// <summary>Real Philippine accommodation category (ADR-0020) — not every stay is a Hotel.</summary>
    public required AccommodationType Type { get; init; }

    /// <summary>Short filterable labels, e.g. "Budget-Friendly", "Family-Friendly" — distinct from the prose Highlights above.</summary>
    public IReadOnlyList<string> Tags { get; init; } = [];

    /// <summary>Nullable — not every budget inn has a formal star rating.</summary>
    public double? OfficialRating { get; init; }

    public required DateTime SourceUpdatedUtc { get; init; }
}
