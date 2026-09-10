namespace Lakbay.Contracts;

/// <summary>
/// Real Philippine accommodation categories (ADR-0020) — Department of
/// Tourism-accredited terms (Hotel, Resort, Apartel, Pension House) plus
/// what's actually listed on Airbnb in these exact destinations today
/// (Hostel, Homestay, Vacation Rental). Not every stay Lakbay sells is a
/// "hotel" — confirmed via live research before adding this field.
/// </summary>
public enum AccommodationType
{
    Hotel,
    Resort,

    /// <summary>Independent furnished apartments leased on a longer-term basis — the DOT term for what Airbnb calls a serviced apartment.</summary>
    Apartel,

    /// <summary>A private/family-run boarding house — DOT's "Pension Home."</summary>
    PensionHouse,
    Hostel,
    Homestay,

    /// <summary>An Airbnb-style entire private home, not run as a formal lodging business.</summary>
    VacationRental,
}
