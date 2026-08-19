# Authentication Flow Foundation — Step 5

This step implements the server-side registration, credential verification, and
opaque-session creation services. It does **not** publish browser-facing
registration/login endpoints or historical account UI.

## Flow

```text
registration request (future boundary)
  -> LegacyUsernamePolicy
  -> AccountRegistrationService
  -> PBKDF2 password hash
  -> user_accounts + password_credentials

login request (future boundary)
  -> AccountSignInService
  -> constant-time credential verification
  -> account state check
  -> secure random opaque session token
  -> SHA-256 token digest saved in user_sessions
```

Only the token digest is persisted. The raw 256-bit token is returned to the
future trusted cookie/session boundary exactly once and must never be logged,
stored in a browser-accessible script, or written to PostgreSQL.

## Deliberate next boundary

The following are intentionally deferred to the next reviewed step:

- authentication endpoints;
- HttpOnly Secure cookie writing;
- cookie validation middleware;
- logout and session revocation;
- registration/login-specific IP and username rate limits;
- CSRF/antiforgery support for form submissions;
- historical 2013 login/sign-up layout.
