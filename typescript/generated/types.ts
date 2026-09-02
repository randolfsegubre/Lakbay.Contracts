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

export enum BoardBasis {
  AllInclusive = 'ALL_INCLUSIVE',
  Breakfast = 'BREAKFAST',
  FullBoard = 'FULL_BOARD',
  HalfBoard = 'HALF_BOARD',
  RoomOnly = 'ROOM_ONLY'
}

export type Destination = {
  __typename?: 'Destination';
  /** ISO 3166-1 alpha-2, e.g. "PH" — never hard-code Philippines in code, only here as data. */
  country: Scalars['String']['output'];
  description: Scalars['String']['output'];
  id: Scalars['ID']['output'];
  latitude?: Maybe<Scalars['Float']['output']>;
  longitude?: Maybe<Scalars['Float']['output']>;
  name: Scalars['String']['output'];
  productLine: ProductLineCode;
  region?: Maybe<Scalars['String']['output']>;
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
  isSoldOut: Scalars['Boolean']['output'];
  itineraryDays: Scalars['Int']['output'];
  name: Scalars['String']['output'];
  priceBands: Array<PriceBand>;
  productLine: ProductLineCode;
  slug: Scalars['String']['output'];
  sourceUpdatedUtc: Scalars['DateTime']['output'];
  summary: Scalars['String']['output'];
};

export type ProductFilter = {
  destinationId?: InputMaybe<Scalars['ID']['input']>;
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
  destinations: Array<Destination>;
  product?: Maybe<Product>;
  productLines: Array<ProductLine>;
  products: Array<Product>;
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
