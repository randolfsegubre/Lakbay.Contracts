namespace Lakbay.Contracts;

/// <summary>
/// Mirrors the Country type in schema/lakbay.graphql. One shared node per
/// platform, not duplicated per ProductLine the way Region/Destination
/// are — Lakbay's whole catalog is one country today, unlike ECMS's
/// real multi-country scale (see ADR-0017).
/// </summary>
public sealed record Country
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    /// <summary>ISO 3166-1 alpha-2, e.g. "PH".</summary>
    public required string Code { get; init; }

    public required string Description { get; init; }
    public IReadOnlyList<string> Highlights { get; init; } = [];
    public required DateTime SourceUpdatedUtc { get; init; }
}
