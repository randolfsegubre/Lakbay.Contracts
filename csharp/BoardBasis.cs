namespace Lakbay.Contracts;

/// <summary>
/// Standard travel-industry meal-inclusion tiers for a Product or RoomType's
/// price band — the same fixed vocabulary hotels/tour operators use
/// worldwide, not a Lakbay-specific invention. Mirrors the BoardBasis enum
/// in schema/lakbay.graphql.
/// </summary>
public enum BoardBasis
{
    /// <summary>No meals included — just the room/stay itself.</summary>
    RoomOnly,
    Breakfast,
    /// <summary>Breakfast plus one other meal (usually dinner).</summary>
    HalfBoard,
    /// <summary>Breakfast, lunch, and dinner.</summary>
    FullBoard,
    /// <summary>All meals plus (typically) drinks and select activities.</summary>
    AllInclusive,
}
