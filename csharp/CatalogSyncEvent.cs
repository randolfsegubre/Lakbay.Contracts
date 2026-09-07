namespace Lakbay.Contracts;

/// <summary>
/// The message Lakbay.Cms publishes to the <c>lakbay-catalog-sync</c>
/// Service Bus queue on content publish, and
/// Lakbay.AvailabilityApi.Sync consumes (ADR-0013). Reuses the same
/// Product/Destination/ProductLine shapes the GraphQL schema already
/// defines instead of inventing a parallel event-only DTO — exactly one of
/// <see cref="ProductLine"/>, <see cref="Destination"/>, or
/// <see cref="Product"/> is populated, selected by <see cref="EntityType"/>.
///
/// Only carries the fields Lakbay.Cms actually owns. For
/// <see cref="Contracts.Product"/> specifically, that means the consumer
/// must never treat <c>AvailableCount</c> here as authoritative — see
/// ADR-0014. Lakbay.Cms always sends a real value for it (there is no
/// "partial" Product on the wire) but Lakbay.AvailabilityApi.Sync's write
/// path deliberately ignores it, so a stale/placeholder count in this
/// event can never clobber what Lakbay.Booking has written.
/// </summary>
public sealed record CatalogSyncEvent
{
    public required CatalogEntityType EntityType { get; init; }
    public ProductLine? ProductLine { get; init; }
    public Destination? Destination { get; init; }
    public Product? Product { get; init; }
    public Country? Country { get; init; }
    public Region? Region { get; init; }
    public Accommodation? Accommodation { get; init; }
    public RoomType? RoomType { get; init; }
    public Activity? Activity { get; init; }
}
