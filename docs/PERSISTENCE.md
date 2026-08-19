# Persistence Foundation — Step 2

This step establishes the first vertical backend capability: asset metadata and
thumbnail delivery-state resolution. It does not accept uploads, serve object
storage, generate thumbnails, run migrations automatically, or contain a
legacy RCC binary.

## Data ownership

PostgreSQL stores metadata only:

- asset identity, type, creator, moderation/public state;
- immutable asset-version storage pointers;
- thumbnail state, selected error code, dimensions, and generated object key.

Object storage will later store original binaries and generated images. Browser
clients never receive raw storage keys as a delivery URL.

## Current endpoint

```text
GET /api/assets/{assetId}/thumbnails/{width}x{height}
```

The endpoint always returns an explicit delivery result. It never delegates to
a browser broken-image icon. A later website adapter will convert each stable
`AssetDeliveryErrorCode` into the verified 2012–2014 image for that state.

## Migration policy

No migration is committed from an unverified local toolchain. After a .NET SDK
is available, generate and inspect the initial migration before applying it:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialAssetDelivery \
  --project src/Roblox.Infrastructure \
  --startup-project src/Roblox.Api \
  --output-dir Persistence/Migrations

dotnet ef database update \
  --project src/Roblox.Infrastructure \
  --startup-project src/Roblox.Api
```

Do not use `EnsureCreated` in this project.
