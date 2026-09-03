# Lakbay.Contracts — Developer Handbook

Written the moment local setup actually worked (2026-09-06) — not from
memory afterward. The test for this document: could you follow it on a
plane, no internet, no AI agent?

## What's in this repo

- `schema/lakbay.graphql` — the SDL. This is the actual source of truth;
  everything else in this repo is generated or hand-mirrored from it.
- `csharp/` — a net10.0 class library (`Lakbay.Contracts.csproj`) with
  POCO records/enums mirroring the schema. Referenced directly (project
  reference, no package registry needed yet) by `Lakbay.Cms`,
  `Lakbay.Booking`, and `Lakbay.AvailabilityApi`.
- `typescript/` — an npm package (`@lakbay/contracts`) using
  `@graphql-codegen` to generate `generated/types.ts` from the schema.
  Consumed by `Lakbay.Web`.

No database, no server, no Docker needed for this repo — it's schema and
generated types only.

## Local setup — proven working, 2026-09-06

Requires: .NET SDK 10.x, Node.js 20+ (built against Node 24.18.0), npm.

```bash
# TypeScript side
cd typescript
npm install
npm run codegen        # writes generated/types.ts from ../schema/lakbay.graphql
npx tsc --noEmit        # type-check, should report zero errors

# C# side
cd ../csharp
dotnet build            # should report 0 Warning(s), 0 Error(s)
```

Both commands were run clean on this machine at scaffold time — if either
fails on a fresh clone, something about the toolchain (not the repo)
has likely changed; check `.NET`/Node versions first.

**2026-09-08 addition:** `package.json` now has explicit `types` and
`exports` fields (`"exports": { ".": "./generated/types.ts" }`) alongside
`main`, added while getting `Lakbay.Web`'s Turbopack build to resolve this
package correctly — see that repo's own handbook for the fuller story
(a `tsconfig.json` `paths` alias plus a widened `turbopack.root` ended up
being the actual fix; the `exports` field alone didn't resolve the
Turbopack-specific issue, but is still worth keeping since it's the
standard way a `"type": "module"` package should declare its entry point).

## Adding a new field to the schema — worked walkthrough

1. Add the field to the relevant type in `schema/lakbay.graphql`, with a
   description string if the field's meaning isn't obvious from its name
   alone.
2. Run `npm run codegen` inside `typescript/` — `generated/types.ts`
   updates automatically. Commit the regenerated file in the same commit
   as the schema change; it's shipped output, not a disposable build
   artifact (see `.gitignore`'s comment on this).
3. Add the matching property by hand to the corresponding record in
   `csharp/*.cs` — there's no C#-from-SDL codegen yet (see README), so
   this step is manual. Keep the C# property name PascalCase even where
   the GraphQL field is camelCase (e.g. `sourceUpdatedUtc` →
   `SourceUpdatedUtc`) — that's the existing convention throughout this
   library.
4. `dotnet build` in `csharp/` to confirm it still compiles.
5. If the field affects `Lakbay.Cms` or `Lakbay.AvailabilityApi`'s
   resolvers, update those repos in a coordinated follow-up — a schema
   change here is a cross-repo event (see `CLAUDE.md`).

## Adding a new type to the schema — worked walkthrough

Same as above, plus: add a new `.cs` file under `csharp/` for the new
type (one file per type/enum is the existing convention — see
`Product.cs`, `Destination.cs`, etc. — not one giant file), and check
whether `Query` in the SDL needs a new field to actually expose it.
