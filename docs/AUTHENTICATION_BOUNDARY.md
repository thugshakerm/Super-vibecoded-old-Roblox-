# Authentication Boundary — Step 6

Step 6 publishes the minimal API authentication boundary. It has no historical
Razor page or page-layout implementation.

## Endpoints

```text
POST /api/auth/register
POST /api/auth/login
POST /api/auth/logout
GET  /api/auth/session
```

All endpoints are protected by a dedicated authentication rate-limit policy:
10 requests per 10 minutes, partitioned by remote IP and endpoint path.

## Cookie behavior

A successful login writes a project-owned opaque `rbx_session` cookie. It is:

- HttpOnly;
- SameSite=Lax;
- Secure outside Development;
- 12 hours maximum lifetime;
- backed by a server-side session record containing only the SHA-256 token
  digest.

The raw opaque session token is never included in a JSON response. Login
failures always return an undifferentiated 401 response, preventing username or
account-state enumeration.

## Important deployment rule

The API and website must use compatible HTTPS deployment origins/cookie domains.
For local development, requests may be made directly against the API's local
origin. Production cookie-domain, proxy, and CSRF configuration must be
reviewed before production deployment.

## Page-source gate

These API endpoints do not authorize implementation of login, registration, or
account pages. The source gate in `HISTORICAL_ACCURACY_RULES.md` remains in
force: obtain a 2012–2014 reference first, or stop and request direction.
