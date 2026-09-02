# Lakbay.Contracts

Shared GraphQL SDL schema + generated TypeScript/C# types for the Lakbay
platform. Consumed by `Lakbay.Cms`, `Lakbay.Booking`, `Lakbay.Web`, and
`Lakbay.AvailabilityApi` — the single source of truth that keeps `Lakbay.Cms`'s
write-model schema and `Lakbay.AvailabilityApi`'s read-model schema from
drifting apart. Both are real, permanently deployed services (see
[ADR-0007](../Lakbay.Docs/docs/adr/ADR-0007-searchapi-is-real-not-mock.md)).

Not yet scaffolded — see [CLAUDE.md](CLAUDE.md) and
[../Lakbay.Docs/docs/02_BUILD_PLAN.md](../Lakbay.Docs/docs/02_BUILD_PLAN.md)
(Phase 0) for what happens next.
