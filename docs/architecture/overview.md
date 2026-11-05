# Architecture overview

ClaimFlow starts as a modular monolith with clear project boundaries:

- **Domain** contains business concepts and rules and has no ClaimFlow project dependencies.
- **Application** coordinates use cases and depends on Domain.
- **Infrastructure** provides external adapters, including persistence, and depends on Application and Domain.
- **Api** is the HTTP entry point and depends on Application and Infrastructure.

The solution is intentionally scaffold-only. No policy, claim, payment, notification, authentication, persistence model, or use-case behavior is implemented yet. See [ADR 0001](../adr/0001-modular-monolith.md) for the modular-monolith decision.
