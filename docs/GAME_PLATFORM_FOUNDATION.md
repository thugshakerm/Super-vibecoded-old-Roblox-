# Game Platform Foundation — Step 9

Step 9 introduces the persistence model required before places, place files,
private servers, launch tickets, and place thumbnails can be implemented.

## Current model

```text
Universe -> Place -> immutable PlaceVersion -> private object-storage key
```

A universe is creator-owned and can contain places. A place has public/private/
group-only access metadata and exactly one root-place flag within its universe.
Place files are private immutable versions, not mutable database blobs.

## Future requirements recorded

The following are required platform capabilities but are intentionally separate
steps:

- private/reserved servers and server-owner permissions;
- join permissions, invites, and paid/private server access;
- game-launch tickets and active-server allocation;
- scoped per-universe DataStores, versioned key/value entries, quotas, and
  controlled server-only access;
- place publishing/upload workflow;
- place thumbnail render jobs through RCC;
- universe and place configuration pages, only after source captures are found.
