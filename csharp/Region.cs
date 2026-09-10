namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the Region type in schema/lakbay.graphql. Embeds its Country
/// in full, same convention as Product embedding Destination — a
/// GraphQL/Mongo consumer never has to issue a second lookup to show
/// "Region, Country" or the country's own highlights.
/// </summary>
public sealed record Region
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    /// <summary>Stable, human-readable join key for the Content tree's regionLandingPage (ADR-0018) — same shape as Product.Slug.</summary>
    public required string Slug { get; init; }

    public required Country Country { get; init; }
    public required ProductLineCode ProductLine { get; init; }
    public required string Description { get; init; }
    public IReadOnlyList<string> Highlights { get; init; } = [];
    public required DateTime SourceUpdatedUtc { get; init; }
}
