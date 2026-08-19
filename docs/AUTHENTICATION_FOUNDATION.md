# Authentication Foundation — Step 4

This is the first identity/persistence slice. It establishes account records and
password credential storage without exposing login, registration, session, or
password-reset endpoints.

## Security decisions

- Password plaintext is accepted only by a future request handler and is never
  represented in the domain entity or database model.
- Password hashes use PBKDF2-SHA512 with a random 128-bit salt, 600,000
  iterations, and constant-time comparison.
- Algorithm and work factor are stored with each credential to permit a future
  controlled password-work-factor upgrade after successful authentication.
- Usernames have a separate normalized form and a case-insensitive uniqueness
  constraint. Exact historical username validation rules will be implemented
  only after the matching 2012–2014 account-creation reference is selected.
- User state is distinct from authentication: Active, UnderReview, Banned, and
  Deleted are persisted but not yet interpreted by authorization middleware.

## Deliberate omissions

This step does not implement: public registration, login, logout, cookies,
sessions, password reset, email verification, rate-limit policy for auth routes,
CSRF, profile pages, or historical sign-up/login markup. Those remain separate
reviewed work.

## Migration

The initial EF migration must be generated after a working .NET SDK is available
and reviewed before application. It will include both the Step 2 asset tables
and these Step 4 identity tables.
