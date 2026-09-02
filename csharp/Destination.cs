namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the Destination type in schema/lakbay.graphql.
/// </summary>
public sealed record Destination
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    /// <summary>ISO 3166-1 alpha-2, e.g. "PH" — never hard-code Philippines in code, only here as data.</summary>
    public required string Country { get; init; }

    public string? Region { get; init; }
    public required ProductLineCode ProductLine { get; init; }
    public required string Description { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public required DateTime SourceUpdatedUtc { get; init; }
}
