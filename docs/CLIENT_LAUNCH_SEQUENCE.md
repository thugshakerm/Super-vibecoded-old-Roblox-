# Steps 21–30: Client Launch Sequence

The ten implementation boundaries are now modeled: client release/channel,
manifest signature, package hashes, install state, launcher authorization,
launcher token handoff, game-server lease, ticket/server assignment boundary,
place-launch negotiation, and non-secret launch audit/failure codes.

The browser sees only the sourced popup and a launcher handoff. It never sees
raw game-server addresses, package storage keys, RCC details, account session
tokens, or a reusable launch credential.
