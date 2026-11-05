# ADR 0001: Start as a modular monolith

- **Status:** Accepted
- **Date:** 2026-11-05

## Context

ClaimFlow is an early-stage insurance policy and claims management platform. The product boundaries, domain ownership, operational load, and team needs are not yet sufficiently understood to justify independently deployed services.

## Decision

Start with a modular monolith organized into Domain, Application, Infrastructure, and Api projects. Keep dependencies explicit and place future capabilities behind cohesive module boundaries. Do not extract microservices until a concrete organizational, scaling, reliability, or deployment need is demonstrated.

## Consequences

- The initial system is simpler to develop, test, run, and operate than a distributed service fleet.
- A single deployable can still contain modules with clear ownership and dependency boundaries.
- Shared process and database deployment mean modules are not independently scalable or failure-isolated.
- Future extraction will require deliberate API/data ownership and migration work; project boundaries alone do not guarantee service independence.
- Extraction decisions should be revisited using measured operational and team constraints rather than anticipated scale.
