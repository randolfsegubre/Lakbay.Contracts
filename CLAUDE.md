# Lakbay.Contracts — Start Here

This file is intentionally short. It exists so any Claude Code session (or
other AI coding assistant) rooted here auto-loads it and is pointed at the
real documentation before touching anything.

**Read, in this order, before writing any code:**

1. [../Lakbay.Docs/docs/01_CLAUDE.md](../Lakbay.Docs/docs/01_CLAUDE.md) —
   the platform AI operating manual.
2. [../Lakbay.Docs/docs/02_BUILD_PLAN.md](../Lakbay.Docs/docs/02_BUILD_PLAN.md)
   — **Phase 0** (schema v0: `Product`, `ProductLine`, `Destination`
   types) is where this repo starts. The schema then evolves alongside
   every later phase that touches `Lakbay.Cms`, `Lakbay.Booking`, or
   `Lakbay.MockApi` — this repo has no phase of its own after Phase 0, it
   changes in step with whichever backend phase is active.
3. [../Lakbay.Docs/docs/04_TASKS.md](../Lakbay.Docs/docs/04_TASKS.md) —
   current status across the whole platform, including the open item on
   which package registry (GitHub Packages vs. Azure Artifacts) this repo
   publishes to.

## What this repo is

The shared GraphQL SDL schema plus generated TypeScript and C# types,
versioned as a package. Deliberately small — schema and generated types
only, never a running service — so it doesn't become a fifth thing to
operate. `Lakbay.Cms` and `Lakbay.MockApi` both implement this same
schema; `Lakbay.Web` and `Lakbay.Booking` consume the generated types.
This is what keeps the real backend and the mock backend from silently
drifting apart (enforced by a schema-diff CI check in `Lakbay.MockApi`).

**Changing this schema is a cross-repo event.** Before merging a schema
change here, check what it breaks in `Lakbay.Cms`'s and
`Lakbay.MockApi`'s resolvers and `Lakbay.Web`'s generated hooks — a schema
field rename or removal is a breaking change for every consumer, not just
a local edit.

## Local setup

Not yet proven — Phase 0 is not complete. Once schema v0 and its codegen
pipeline exist, the exact commands go in `Docs/DEVELOPER_HANDBOOK.md`
(create that file the moment setup actually works, not from memory
afterward).

## End of session

Update `../Lakbay.Docs/docs/04_TASKS.md` and append an entry to
`../Lakbay.Docs/docs/05_DEVLOG.md` for anything that changed phase status
or made a new structural decision.
