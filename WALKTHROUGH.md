# Code Walkthrough — Lakbay.Contracts

This repo has **no running service** - it's the shared vocabulary every
other Lakbay repo agrees to speak. This walkthrough explains what's here,
why it's shaped this way, and how a change here ripples out to the rest of
the platform (see the platform-level `Lakbay.Docs/WALKTHROUGH.md` for how
the repos fit together end to end).

## Why this repo exists at all

`Lakbay.Cms` (the write side, Umbraco) and `Lakbay.AvailabilityApi` (the
read side, MongoDB/GraphQL) are two **independent, real, permanently
deployed services** implementing the *same* catalog shape - not a real
backend and a disposable mock. Without a shared contract, their two
implementations of "what is a Product" would drift apart silently over
time. This repo is that shared contract, versioned as a package three
different ways:

```
schema/lakbay.graphql          The source of truth - a plain GraphQL SDL file
csharp/*.cs                    Hand-written C# records mirroring the schema
typescript/generated/types.ts  Auto-generated TS types (npm run codegen)
```

**Why hand-written C# but generated TypeScript?** There's no SDL-to-C#
codegen pipeline yet (a known, tracked gap - see `Lakbay.Docs/docs/04_TASKS.md`).
Every time `schema/lakbay.graphql` changes, the matching `csharp/*.cs`
record has to be updated by hand to match, field-for-field - nothing
enforces that mechanically today, so a mismatch is a real, easy mistake to
make (this session found and fixed exactly that kind of mismatch - see
`ProductFilter.DestinationId`'s doc comment).

## Reading one type across all three representations

Take `Country` (added this session, ADR-0017) as the concrete example:

1. **`schema/lakbay.graphql`** declares the GraphQL shape:
   ```graphql
   type Country {
     id: ID!
     name: String!
     code: String!
     description: String!
     highlights: [String!]!
     sourceUpdatedUtc: DateTime!
   }
   ```
2. **`csharp/Country.cs`** mirrors it as a C# record, by hand:
   ```csharp
   public sealed record Country
   {
       public required string Id { get; init; }
       public required string Name { get; init; }
       // ...
   }
   ```
   `Lakbay.Cms` (`CatalogPublishSyncHandler.BuildCountry`) constructs one of
   these from Umbraco content; `Lakbay.AvailabilityApi.Sync`
   (`CatalogSyncFunction.ApplyCountryAsync`) writes it to MongoDB;
   `Lakbay.AvailabilityApi.Api` (`Query.GetCountry`) reads it back out.
3. **`typescript/generated/types.ts`** is generated (`npm run codegen` in
   `typescript/`, driven by `codegen.ts`) - never hand-edited. `Lakbay.Web`
   imports this type to know what shape a GraphQL response has, with real
   compile-time checking instead of `any`.

## The one non-obvious rule: `sourceUpdatedUtc` is not a normal timestamp

Every type here carries a `sourceUpdatedUtc: DateTime!` field. This isn't
"last modified" for display purposes - it's the field
`Lakbay.AvailabilityApi.Sync`'s last-write-wins guard (ADR-0010) compares
against before applying any update, so an out-of-order Service Bus message
delivery can never overwrite newer data with stale data. `Product`
specifically has a *second*, internal-only timestamp for this same reason
(ADR-0014) - see that file's own comment for why one field wasn't enough
once `Product` gained two independent writers (`Lakbay.Cms` for catalog
fields, `Lakbay.Booking` for `AvailableCount`).

## `ID` vs `String` - the recurring gotcha in this schema

Several fields that conceptually *are* an id (`ProductFilter.DestinationId`,
`RoomType.AccommodationId`, the `destinationId`/`accommodationId` query
arguments) are deliberately typed `String`, not GraphQL's `ID` scalar. This
looks wrong at first glance - it's actually working around a real
HotChocolate (the GraphQL server library `Lakbay.AvailabilityApi` uses)
behavior: it only infers a C# member as GraphQL `ID` when that member is
literally named `Id`, so anything suffixed (`DestinationId`,
`AccommodationId`) would silently serve as `String` at runtime regardless
of what the SDL says. Typing it `String` here keeps the hand-written
contract honest about what the server actually does. See ADR-0019 for the
full story of how this was found.

## Making a schema change - the actual checklist

1. Edit `schema/lakbay.graphql` first - it's the source of truth.
2. Update the matching `csharp/*.cs` record by hand.
3. `cd typescript && npm run codegen` to regenerate the TS types.
4. Check every implementer: `Lakbay.Cms`'s content types/seeders/publish-sync
   handler, `Lakbay.AvailabilityApi`'s Mongo class maps/sync function/
   resolvers, and `Lakbay.Web`'s GraphQL query strings and components.
   A field rename or removal here is a breaking change for all of them at
   once, not a local edit.
