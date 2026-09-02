namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the ProductLine type in schema/lakbay.graphql.
/// </summary>
public sealed record ProductLine
{
    public required ProductLineCode Code { get; init; }
    public required string Name { get; init; }
    public required string Tagline { get; init; }

    /// <summary>
    /// ISO 3166-1 alpha-2 country codes this product line currently
    /// covers. Starts as ["PH"] — deliberately data, not hard-coded
    /// application logic, so a later SEA expansion is a content change.
    /// </summary>
    public required IReadOnlyList<string> Countries { get; init; }

    /// <summary>
    /// Set by whichever service last wrote this record. AvailabilityApi's
    /// sync consumer only applies an update if this is newer than what it
    /// already has — see ADR-0010.
    /// </summary>
    public required DateTime SourceUpdatedUtc { get; init; }
}
