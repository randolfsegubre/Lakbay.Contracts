# Lakbay.Contracts

Shared GraphQL SDL schema + generated TypeScript/C# types for the Lakbay
platform. Consumed by `Lakbay.Cms`, `Lakbay.Booking`, `Lakbay.Web`, and
`Lakbay.AvailabilityApi` — the single source of truth that keeps `Lakbay.Cms`'s
write-model schema and `Lakbay.AvailabilityApi`'s read-model schema from
drifting apart. Both are real, permanently deployed services (see
[ADR-0007](../Lakbay.Docs/docs/adr/ADR-0007-searchapi-is-real-not-mock.md)).

## Layout

```
schema/lakbay.graphql   the SDL — source of truth for the whole platform
csharp/                 Lakbay.Contracts.csproj — POCO records mirroring the schema
                         (net10.0), referenced directly by Lakbay.Cms,
                         Lakbay.Booking, Lakbay.AvailabilityApi during local dev
typescript/              @lakbay/contracts — graphql-codegen setup;
                         generated/types.ts is committed (it's the shipped
                         output, not a build artifact) and consumed by Lakbay.Web
```

## Local setup

```bash
# TypeScript types — regenerate after any schema/lakbay.graphql change
cd typescript
npm install
npm run codegen

# C# types — just build; no codegen step, types are hand-written to mirror
# the SDL (see csharp/README note below if that ever changes)
cd ../csharp
dotnet build
```

**Keeping C# and TypeScript in sync with the SDL:** the TypeScript side is
generated (`npm run codegen`); the C# side is currently hand-written to
mirror `schema/lakbay.graphql` — there's no C#-from-SDL codegen step yet.
When you change the schema, update `csharp/*.cs` by hand in the same
commit. A schema-diff CI check (Phase 0 task) should eventually catch
drift on both sides.

Phase 0 status and what's next: [CLAUDE.md](CLAUDE.md) and
[../Lakbay.Docs/docs/02_BUILD_PLAN.md](../Lakbay.Docs/docs/02_BUILD_PLAN.md).
