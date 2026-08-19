# ROBLOX 2012–2014 Revival

A C#-first, historically referenced platform recreation. The project preserves
period-authentic presentation while implementing safe modern server behavior.

## First scaffold

```text
src/
  Roblox.Website/       browser-facing Razor frontend (runs separately)
  Roblox.Api/           platform backend API (runs separately)
  Roblox.Domain/        domain contracts and enums
  Roblox.Application/   use-case contracts
  Roblox.Infrastructure/ storage and integration boundary
  Roblox.Workers/       non-RCC background worker host
  RccService/           internal RCC adapter host; no legacy binary included
references/             source metadata only; no snapshot account data
```

## Requirements

- .NET SDK 10.0.100 or newer feature-band compatible SDK
- Docker Desktop or another Docker-compatible runtime for PostgreSQL

For reproducible local SDK setup without a system-wide installation, run:

```bash
./scripts/bootstrap-dotnet.sh
```

Then replace `dotnet` with `./scripts/dotnet.sh` in the commands below. See
`docs/DOTNET_SETUP.md`.

## Local setup

### Windows quick start

After installing Docker Desktop and the .NET 10 SDK, double-click `run.bat` or run:

```bat
run.bat
```

It creates a local `.env` from `.env.example` when needed, starts PostgreSQL,
restores/builds the solution, and opens separate API and Website command windows.

For an already-running native PostgreSQL instance instead of Docker:

```bat
set USE_EXISTING_POSTGRES=1 && run.bat
```

### Manual setup

1. Copy `.env.example` to `.env` and set a local `POSTGRES_PASSWORD`.
2. Start only PostgreSQL:
   ```bash
   docker compose up -d postgres
   ```
3. Restore/build the solution:
   ```bash
   dotnet restore Roblox.Revival.sln
   dotnet build Roblox.Revival.sln
   ```
4. If you changed `POSTGRES_PASSWORD`, give the API the matching connection string in the terminal where it will run:
   ```bash
   export ConnectionStrings__Roblox="Host=localhost;Port=5432;Database=roblox_revival;Username=roblox;Password=$POSTGRES_PASSWORD"
   ```
   On PowerShell, use `$env:ConnectionStrings__Roblox = "..."` instead.
5. Run the browser-facing frontend in one terminal:
   ```bash
   dotnet run --project src/Roblox.Website
   ```
6. Run the backend API in a second terminal:
   ```bash
   dotnet run --project src/Roblox.Api
   ```

Health endpoints:

```text
Website: http://localhost:5100/health
API:     http://localhost:5200/health
API:     http://localhost:5200/api/status
```

No historical page layout has been implemented in this scaffold. See
`HISTORICAL_ACCURACY_RULES.md` and `docs/ARCHITECTURE.md`.

## Step 2: asset metadata and thumbnail-state foundation

The API now contains the first reviewed backend capability: asset metadata and
thumbnail delivery-state resolution. See `docs/PERSISTENCE.md`. It requires an
EF Core migration before database-backed requests can run; the migration is
intentionally not hand-written or applied without a verified .NET toolchain.

## Step 3: separate-process API security

The frontend and API now have an explicit local-development origin contract,
server-side API client configuration, API rate limiting, security headers, and
safe API error responses. See `docs/API_SECURITY.md` before changing origins or
hosting configuration.

## Step 4: identity and password foundation

The domain now has account lifecycle records and secure password-hash service
contracts, but no public authentication endpoints. See
`docs/AUTHENTICATION_FOUNDATION.md`.

## Step 5: authentication flow foundation

Server-side registration, sign-in credential verification, opaque-session
creation, and persistence contracts are now in place. Browser-facing auth
endpoints/cookies and historical login UI are deliberately deferred. See
`docs/AUTHENTICATION_FLOW.md`.

## Step 6: API authentication boundary

The API now has registration, login, logout, and session endpoints backed by
opaque HttpOnly sessions and dedicated auth rate limiting. No historical
page/layout was added. See `docs/AUTHENTICATION_BOUNDARY.md`.

## Steps 7–8: private asset storage and RCC render-job foundation

Private object-storage abstractions and typed, persistent RCC render jobs are
now in place. They do not expose uploads, public storage URLs, or an RCC
binary. See `docs/ASSET_STORAGE.md` and `docs/RCC_RENDER_JOBS.md`.

## Step 9: universe and place foundation

Universes, places, immutable place versions, and private storage-key conventions
are now modeled. Private servers and DataStores are recorded as required future
capabilities, not omitted. See `docs/GAME_PLATFORM_FOUNDATION.md` and
`docs/ROADMAP.md`.

## Steps 10–12: server, DataStore, and render-worker boundaries

Private-server/launch-ticket models, universe-scoped server-only DataStore
models, and the isolated render-worker boundary are now documented and modeled.
No running game server, DataStore endpoint, or RCC binary is exposed yet. See
`docs/PRIVATE_SERVERS_AND_LAUNCH.md`, `docs/DATASTORES.md`, and
`docs/RENDER_WORKER_BOUNDARY.md`.

## Steps 13–15: VIP codes, launch consumption, and render dispatch preparation

The supplied PHP-compatible VIP link-code algorithm is implemented without raw
code persistence. Launch tickets now have a consume transition; render dispatch
remains isolated pending RCC validation. See `docs/VIP_LINK_CODES.md`,
`docs/LAUNCH_TICKET_CONSUMPTION.md`, and `docs/STEP_15_RENDER_DISPATCH.md`.

## Steps 16–20: client deployment and economy foundations

Client release manifests, bootstrapping/launcher boundaries, immutable currency
ledger models, limited serial ownership, completed sale history, and isolated
RAP calculation contracts are now modeled. The visible Play popup remains
blocked until an exact historical reference is acquired. See
`docs/LAUNCHER_AND_BOOTSTRAPPER.md` and `docs/ECONOMY_AND_LIMITEDS.md`.

## Steps 21–30: launcher/bootstrapper contracts

Client installation state, signed-manifest contracts, launcher authorization,
game-server lease records, place-launch negotiation, and launch audit/failure
boundaries are modeled. See `docs/CLIENT_LAUNCH_SEQUENCE.md`.

## Steps 31–33: asset upload intake

Private, expiring upload-intake records, conservative file-type/size policy,
and quarantine storage keys are modeled. See `docs/ASSET_UPLOAD_INTAKE.md`.

## Step 34: authenticated asset-upload intake API

Authenticated creators can now request private upload intake records without
receiving storage keys. See `docs/ASSET_UPLOAD_API.md`.

## Frontend phase: source-approved launcher component

The first Razor UI component is the exact archived 2013 Place Launcher status
fragment and its sourced progress asset. It is not yet hosted by a page. See
`docs/PLACE_LAUNCHER_COMPONENT.md`.

## Frontend phase: 2013 Home navigation

The raw 2013 Home source is retained under `references/Home/`; its sanitized,
runtime-bound navigation partial is in `Pages/Shared/Navigation/Navigation.cshtml`.
See `docs/HOME_NAVIGATION_COMPONENT.md`.

## Frontend phase: 2013 Home footer

The sanitized 2013 Home footer component is now available under
`Pages/Shared/Navigation/Footer.cshtml`. See `docs/HOME_FOOTER_COMPONENT.md`.

## Frontend phase: 2013 Home dashboard components

Source-derived runtime-bound Home header, news, best-friends, recently-played,
and column components are available under `Pages/Shared/Home/`. See
`docs/HOME_DASHBOARD_COMPONENTS.md`.

## Frontend phase: 2013 Login and Signup pages

Source-derived Razor Login and Signup pages now use the existing authentication
API through the website's server-side API client. See `docs/LOGIN_SIGNUP_COMPONENTS.md`.
