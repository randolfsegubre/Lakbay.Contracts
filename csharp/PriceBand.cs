namespace Lakbay.Contracts;

public sealed record PriceBand
{
    public required string Label { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
    public required decimal PricePhp { get; init; }
}
