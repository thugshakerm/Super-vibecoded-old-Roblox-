# Bootstrapper, Launcher, and Play Flow Research — Steps 16–18

The archive contains a dedicated `RobloxPlayerLauncher` collection and multiple
2013 RobloxPlayer release folders. These are the selected client-side reference
sources. The implementation is split into:

1. **Bootstrapper:** verifies a selected client manifest and package hashes.
2. **Launcher:** accepts an authorized, short-lived launch ticket and starts the
   selected installed client.
3. **Play popup:** browser-facing visual UI; it is blocked by the page-source
gate until an exact 2012–2014 popup reference is acquired.

No executable has been imported or run. No substitute popup design is allowed.
