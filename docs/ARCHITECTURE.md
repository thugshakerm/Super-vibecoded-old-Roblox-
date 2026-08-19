# Scaffold Architecture

## Process boundaries

- **Roblox.Website** is the browser-facing Razor application. It will render the
  approved, sanitized historical layout and call the API through configured URLs.
- **Roblox.Api** owns public platform APIs and domain use cases.
- **Roblox.Workers** owns non-RCC background jobs.
- **RccService** is an internal C# adapter host for an isolated legacy Windows
  RCC worker. It is not the legacy executable and does not publish a public port.

## Dependency direction

`Website / Api / Workers / RccService -> Infrastructure -> Application -> Domain`

The Domain project has no infrastructure dependency. The RCC executable, client
archives, original binary assets, and account snapshot data are intentionally
not part of this scaffold.

## Placeholder contract

`AssetDeliveryErrorCode` is the single stable source of the separate fallback
states. The later website implementation must map each code to a verified
period-specific asset and never use browser broken-image behavior.
