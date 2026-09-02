namespace Lakbay.Contracts;

/// <summary>
/// One of Lakbay's four themed holiday collections. Mirrors the
/// ProductLineCode enum in schema/lakbay.graphql — keep the two in sync
/// by hand until codegen exists for this side (see the Contracts README).
/// </summary>
public enum ProductLineCode
{
    /// <summary>Islands &amp; water adventure — Palawan, Siargao, Boracay, Cebu, Bohol.</summary>
    Alon,

    /// <summary>Highlands &amp; cool-climate escapes — Baguio, Sagada, Mt. Pulag, Tagaytay.</summary>
    Amihan,

    /// <summary>Festive / light tourism — Pampanga's Giant Lantern Festival, Panagbenga, Sinulog.</summary>
    Parul,

    /// <summary>Heritage &amp; culture — Vigan, Ifugao Rice Terraces, Intramuros.</summary>
    Pamana,
}
