# Historical Accuracy Rules

## Scope
This project recreates the 2012–2014 ROBLOX website experience from archived
period references. A reference capture establishes the visual and structural
source of truth for the page it documents.

## Non-negotiable implementation rule
**Do not invent, redesign, modernize, or "improve" historical page layouts,
HTML structure, navigation, sidebars, controls, copy, or visual treatments.**

When a historical page is implemented:

1. Start from the selected period capture and preserve its layout hierarchy,
   information architecture, page regions, control placement, terminology, and
   visual language.
2. Remove archive wrappers, trackers, obsolete advertising/analytics code,
   dead external embeds, and non-functional historic transport code.
3. Remove all snapshot-owner and snapshot-specific data before use. This
   includes usernames, user IDs, avatar thumbnails, balances, notification
   counts, friends/best friends, feed posts, recently played games, inventory,
   groups, messages, timestamps, personal links, and any other cached account
   data.
4. Replace removed personal data only with runtime-bound application data or a
   historically supported empty-state. Do not create a new visual design to
   fill removed content.
5. Preserve the original period layout for empty states: panels, headings,
   dimensions, separators, buttons, and placement must remain faithful to the
   source capture.
6. Use safe modern server-side implementations for authentication,
   authorization, CSRF protection, input validation, sessions, asset access,
   and RCC isolation. Security improvements must not alter the visible
   historical design unless there is no safe alternative.

## Universal shell
The navbar and contextual sidebars are universal shared components. Their
structure and styling must be sourced from the chosen late-2013 reference and
used consistently across the site. They must not be independently redesigned
for individual pages.

## Source traceability
Every implemented page must record its archive URL, capture date, target era,
and a summary of removed snapshot-specific data in its implementation notes.

## Page-source gate
No website page, page layout, Razor markup, or replacement historical UI may be
created without a verified 2012–2014 source capture for that specific page or
shared component. If a required source cannot be found, stop and request user
direction; do not infer, combine unrelated captures, or create an approximation.
