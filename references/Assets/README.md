# Extracted Period UI Assets

These are reference-only UI assets extracted from the repository's MHTML/Wayback
captures. They are organized for later historically sourced implementation.

Included categories:

- `badges`: period badge graphics;
- `membership`: Builders Club/Turbo Builders Club/Outrageous Builders Club and
  related access overlays;
- `navigation`: period logo, menu, genre, and UI icon sprites;
- `status-icons`: period success/warning icon graphics.

Excluded deliberately:

- user avatars;
- user/group/item/place/game thumbnails;
- screenshot content;
- ads;
- Wayback Machine toolbar/interface assets;
- unrelated archive assets.

`manifest.json` records each output file's original archive location, capture
source, MIME type, and SHA-256 hash. No asset should be used as a page design
reference outside the era/source documented in that manifest.

## SVG note

The local MHTML captures contain only Wayback/archive SVG interface files, not
original ROBLOX SVG UI assets. Those archive SVGs were intentionally excluded.
The original period UI assets selected here are primarily PNG sprites/overlays,
which is consistent with the captured 2012–2013 site material.

## Expanded hardcoded UI extraction

`period-ui/` contains the remaining hardcoded original `www.roblox.com/images/`
assets embedded by the captures, including old buttons, headers, tabs, form/UI
sprites, status graphics, membership overlays, and related fixed interface
artwork. Thumbnail-CDN hosts and user/content thumbnails remain excluded.

The exact May 2013 launcher popup is separately preserved in
`../PlaceLauncher/` with its original markup fragment, CSS fragment, and
progress GIF.
