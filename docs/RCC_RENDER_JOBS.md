# RCC Render Job Foundation — Step 8

Render jobs are stored in `platform.render_jobs`. They carry typed, trusted
parameters only and do not store Lua.

Supported job kinds:

- `AvatarFullBody` — use only in slots confirmed by the source page;
- `AvatarHeadshot` — close-up render, use only in confirmed headshot slots;
- `AssetThumbnail`;
- `PlaceThumbnail`.

The actual legacy RCC binary is deliberately not connected yet. `RccService`
contains only the internal adapter contract until isolated Windows-worker
validation is complete.
