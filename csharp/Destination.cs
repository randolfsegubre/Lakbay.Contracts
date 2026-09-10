namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the Destination type in schema/lakbay.graphql.
/// </summary>
public sealed record Destination
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    /// <summary>Stable, human-readable join key for the Content tree's destinationLandingPage (ADR-0018) — same shape as Product.Slug.</summary>
    public required string Slug { get; init; }

    /// <summary>ISO 3166-1 alpha-2, e.g. "PH" — never hard-code Philippines in code, only here as data.</summary>
    public required string Country { get; init; }

    public required Region Region { get; init; }
    public required ProductLineCode ProductLine { get; init; }
    public required string Description { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }

    /// <summary>Resort-wide perks bundled into any stay here, regardless of which Accommodation — e.g. "Round-trip transfers included." See the 2026-09-08 Inghams research and its Destination-vs-Product scope decision.</summary>
    public IReadOnlyList<string> IncludedPerks { get; init; } = [];

    /// <summary>Resort-wide bookable extras, not included in any price band.</summary>
    public IReadOnlyList<string> OptionalAddOns { get; init; } = [];

    public required DateTime SourceUpdatedUtc { get; init; }
}
