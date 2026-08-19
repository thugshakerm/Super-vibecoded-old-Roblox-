# API and Website Security Foundation — Step 3

The website and API remain independent processes. The browser-facing website is
configured with a server-side named `HttpClient` for the API; it does not put an
internal service address into page markup.

## Cross-origin policy

The API accepts browser cross-origin requests only from the explicit
`ApiSecurity:FrontendOrigins` list. There is no wildcard origin policy. The
policy allows credentials because later authenticated historical pages require
cookie-based sessions; this is safe only with explicit origins.

For production, set the exact HTTPS website origin through configuration, for
example:

```text
ApiSecurity__FrontendOrigins__0=https://www.example.com
PlatformApi__BaseUrl=https://api.example.com
```

## Baseline protections

- RFC 7807-style problem responses for unexpected API failures;
- fixed-window per-IP API rate limiting;
- no CORS wildcard;
- security headers on website and API responses;
- health endpoint separate from functional public API endpoints;
- no legacy RCC port, object-storage key, or secret exposed to browsers.

## Deliberate omissions

Authentication, authorization policies, antiforgery, user records, asset
uploads, object storage, and historical UI are separate reviewed steps.
