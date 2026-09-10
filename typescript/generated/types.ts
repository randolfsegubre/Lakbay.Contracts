export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** An RFC 3339 date-time string, e.g. 2026-09-06T12:00:00Z. */
  DateTime: { input: string; output: string; }
};

/**
 * The specific place guests stay — promoted from Product's flat
 * accommodationName/accommodationDescription strings into a real, reusable
 * entity with its own highlights (ADR-0017, resolving the "revisit only
 * if..." case ADR-0016 explicitly flagged).
 */
export type Accommodation = {
  __typename?: 'Accommodation';
  description: Scalars['String']['output'];
  /** Was only reachable indirectly via Product before — added so Accommodation can be browsed/searched on its own (the Stays page, ADR-0019). */
  destination: Destination;
  heroImageUrl?: Maybe<Scalars['String']['output']>;
  highlights: Array<Scalars['String']['output']>;
  id: Scalars['ID']['output'];
  name: Scalars['String']['output'];
  /** Nullable — not every budget inn has a formal star rating. */
  officialRating?: Maybe<Scalars['Float']['output']>;
  sourceUpdatedUtc: Scalars['DateTime']['output'];
  /** Short filterable labels, e.g. "Budget-Friendly", "Family-Friendly" — distinct from the prose highlights above. */
  tags: Array<Scalars['String']['output']>;
  /** Real Philippine accommodation category (ADR-0020) — not every stay is a Hotel. */
  type: AccommodationType;
};

/**
 * Real Philippine accommodation categories (ADR-0020) — Department of
 * Tourism-accredited terms plus what's actually listed on Airbnb in these
 * destinations today. Not every stay Lakbay sells is a Hotel.
 */
export enum AccommodationType {
  /** Independent furnished apartments leased on a longer-term basis. */
  Apartel = 'APARTEL',
  Homestay = 'HOMESTAY',
  Hostel = 'HOSTEL',
  Hotel = 'HOTEL',
  /** A private/family-run boarding house — DOT's "Pension Home." */
  PensionHouse = 'PENSION_HOUSE',
  Resort = 'RESORT',
  /** An Airbnb-style entire private home, not run as a formal lodging business. */
  VacationRental = 'VACATION_RENTAL'
}

/**
 * A real, independently bookable local activity at a fixed, agreed-upfront
 * price (ADR-0020) — the direct answer to "buying from a local risks
 * getting scammed or overpriced." Modeled on the real Klook/GetYourGuide
 * pattern: a fixed price and every inclusion itemized in writing.
 */
export type Activity = {
  __typename?: 'Activity';
  description: Scalars['String']['output'];
  destination: Destination;
  /** Descriptive, e.g. "Half-day (4 hours)" — not a precise ISO duration. */
  durationLabel: Scalars['String']['output'];
  heroImageUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['ID']['output'];
  /** Itemized inclusions (guide, gear, fees, meals). */
  includes: Array<Scalars['String']['output']>;
  name: Scalars['String']['output'];
  /** Fixed, per-person, agreed upfront — the whole point. */
  pricePhp: Scalars['Float']['output'];
  sourceUpdatedUtc: Scalars['DateTime']['output'];
};

export enum BoardBasis {
  AllInclusive = 'ALL_INCLUSIVE',
  Breakfast = 'BREAKFAST',
  FullBoard = 'FULL_BOARD',
  HalfBoard = 'HALF_BOARD',
  RoomOnly = 'ROOM_ONLY'
}

/**
 * A single shared node per platform, not duplicated per ProductLine the
 * way Region/Destination are — Lakbay's whole catalog is one country today
 * (see ADR-0017's "shared Country" decision).
 */
export type Country = {
  __typename?: 'Country';
  /** ISO 3166-1 alpha-2, e.g. "PH". */
  code: Scalars['String']['output'];
  description: Scalars['String']['output'];
  highlights: Array<Scalars['String']['output']>;
  id: Scalars['ID']['output'];
  name: Scalars['String']['output'];
  sourceUpdatedUtc: Scalars['DateTime']['output'];
};

export type Destination = {
  __typename?: 'Destination';
  /** ISO 3166-1 alpha-2, e.g. "PH" — never hard-code Philippines in code, only here as data. */
  country: Scalars['String']['output'];
  description: Scalars['String']['output'];
  id: Scalars['ID']['output'];
  /** Resort-wide perks bundled into any stay here, regardless of which Accommodation — e.g. "Round-trip transfers included." See ADR-0019. */
  includedPerks: Array<Scalars['String']['output']>;
  latitude?: Maybe<Scalars['Float']['output']>;
  longitude?: Maybe<Scalars['Float']['output']>;
  name: Scalars['String']['output'];
  /** Resort-wide bookable extras, not included in any price band. */
  optionalAddOns: Array<Scalars['String']['output']>;
  productLine: ProductLineCode;
  region: Region;
  /** Stable join key for the Content tree's destinationLandingPage (ADR-0018). */
  slug: Scalars['String']['output'];
  sourceUpdatedUtc: Scalars['DateTime']['output'];
};

export type PriceBand = {
  __typename?: 'PriceBand';
  endDate: Scalars['DateTime']['output'];
  label: Scalars['String']['output'];
  pricePhp: Scalars['Float']['output'];
  startDate: Scalars['DateTime']['output'];
};

export type Product = {
  __typename?: 'Product';
  /** The specific place guests stay. Null only for a handful of pre-restructure legacy products. */
  accommodation?: Maybe<Accommodation>;
  /**
   * Current bookable count. Lakbay.Booking is the only writer of the real
   * value (ADR-0011); this field on Lakbay.AvailabilityApi's copy is a
   * read-model mirror kept current via AvailabilityChanged events
   * (ADR-0008), not a second source of truth.
   */
  availableCount: Scalars['Int']['output'];
  boardBasis: BoardBasis;
  destination: Destination;
  heroImageUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['ID']['output'];
  /** Bundled into the price band above — no separate charge. */
  includedActivities: Array<Scalars['String']['output']>;
  isSoldOut: Scalars['Boolean']['output'];
  itineraryDays: Scalars['Int']['output'];
  name: Scalars['String']['output'];
  /** Bookable add-ons on top of the package, not included in priceBands. */
  optionalActivities: Array<Scalars['String']['output']>;
  priceBands: Array<PriceBand>;
  productLine: ProductLineCode;
  slug: Scalars['String']['output'];
  sourceUpdatedUtc: Scalars['DateTime']['output'];
  summary: Scalars['String']['output'];
};

export type ProductFilter = {
  destinationId?: InputMaybe<Scalars['String']['input']>;
  endDate?: InputMaybe<Scalars['DateTime']['input']>;
  excludeSoldOut?: InputMaybe<Scalars['Boolean']['input']>;
  maxPricePhp?: InputMaybe<Scalars['Float']['input']>;
  minPricePhp?: InputMaybe<Scalars['Float']['input']>;
  productLine?: InputMaybe<ProductLineCode>;
  startDate?: InputMaybe<Scalars['DateTime']['input']>;
};

export type ProductLine = {
  __typename?: 'ProductLine';
  code: ProductLineCode;
  /**
   * ISO 3166-1 alpha-2 country codes this product line currently covers.
   * Starts as ["PH"] — deliberately data, not hard-coded in application
   * code, so a later Southeast-Asia expansion is a content change, not a
   * rebuild (see the Blueprint's market-research section).
   */
  countries: Array<Scalars['String']['output']>;
  name: Scalars['String']['output'];
  /**
   * Set by whichever service last wrote this record — Lakbay.Cms on
   * authoring changes. Lakbay.AvailabilityApi's sync consumer only applies
   * an update if this is newer than what it already has (ADR-0010).
   */
  sourceUpdatedUtc: Scalars['DateTime']['output'];
  tagline: Scalars['String']['output'];
};

/**
 * One of Lakbay's four themed holiday collections (working names — final
 * branding is a marketing decision, not a technical one; see the Lakbay
 * Blueprint). Code is the stable identifier — key logic off this, not the
 * display name.
 */
export enum ProductLineCode {
  /** Islands & water adventure — Palawan, Siargao, Boracay, Cebu, Bohol */
  Alon = 'ALON',
  /** Highlands & cool-climate escapes — Baguio, Sagada, Mt. Pulag, Tagaytay */
  Amihan = 'AMIHAN',
  /** Heritage & culture — Vigan, Ifugao Rice Terraces, Intramuros */
  Pamana = 'PAMANA',
  /** Festive / light tourism — Pampanga's Giant Lantern Festival, Panagbenga, Sinulog */
  Parul = 'PARUL'
}

export type Query = {
  __typename?: 'Query';
  /** Cross-destination hotel search (ADR-0019) — the Stays page's main query. */
  accommodations: Array<Accommodation>;
  /** Fair-price local activities marketplace (ADR-0020), independent of any Product package. */
  activities: Array<Activity>;
  /** Singular — one shared Country node today (ADR-0017). */
  country?: Maybe<Country>;
  destinations: Array<Destination>;
  product?: Maybe<Product>;
  productLines: Array<ProductLine>;
  products: Array<Product>;
  regions: Array<Region>;
  roomTypes: Array<RoomType>;
};


export type QueryAccommodationsArgs = {
  destinationId?: InputMaybe<Scalars['String']['input']>;
  tag?: InputMaybe<Scalars['String']['input']>;
};


export type QueryActivitiesArgs = {
  destinationId?: InputMaybe<Scalars['String']['input']>;
};


export type QueryDestinationsArgs = {
  productLine?: InputMaybe<ProductLineCode>;
};


export type QueryProductArgs = {
  slug: Scalars['String']['input'];
};


export type QueryProductsArgs = {
  filter?: InputMaybe<ProductFilter>;
};


export type QueryRegionsArgs = {
  productLine?: InputMaybe<ProductLineCode>;
};


export type QueryRoomTypesArgs = {
  accommodationId: Scalars['String']['input'];
};

/**
 * One per ProductLine, like Destination — e.g. "Palawan" for Islands,
 * "Cordillera Administrative Region" for Highlands. See ADR-0017.
 */
export type Region = {
  __typename?: 'Region';
  country: Country;
  description: Scalars['String']['output'];
  highlights: Array<Scalars['String']['output']>;
  id: Scalars['ID']['output'];
  name: Scalars['String']['output'];
  productLine: ProductLineCode;
  /** Stable join key for the Content tree's regionLandingPage (ADR-0018). */
  slug: Scalars['String']['output'];
  sourceUpdatedUtc: Scalars['DateTime']['output'];
};

/**
 * A real "pick your room" option within an Accommodation (ADR-0019) — see
 * the Inghams "Room Types" pattern (Hotel Post, St Anton: "St Anton room,"
 * "Galzig room," each its own size/bed configuration/occupancy/price).
 * Queried by accommodationId, not embedded on Accommodation — same shape
 * Query.products(filter: {destinationId}) already uses.
 */
export type RoomType = {
  __typename?: 'RoomType';
  accommodationId: Scalars['ID']['output'];
  bedConfiguration: Scalars['String']['output'];
  boardBasis: BoardBasis;
  description: Scalars['String']['output'];
  /** Falls back to the owning Accommodation's own photo in the UI rather than a fabricated distinct-room photo. */
  heroImageUrl?: Maybe<Scalars['String']['output']>;
  id: Scalars['ID']['output'];
  maxOccupancy: Scalars['Int']['output'];
  /** Nullable — only set where a longer stay is realistic (ADR-0020). A discounted flat monthly price, not a date-ranged PriceBand. */
  monthlyRatePhp?: Maybe<Scalars['Float']['output']>;
  name: Scalars['String']['output'];
  priceBands: Array<PriceBand>;
  /** Descriptive range, e.g. "18-22m²" — matches real hotel copy, not a precise measurement. */
  sizeSqm?: Maybe<Scalars['String']['output']>;
  sourceUpdatedUtc: Scalars['DateTime']['output'];
};
