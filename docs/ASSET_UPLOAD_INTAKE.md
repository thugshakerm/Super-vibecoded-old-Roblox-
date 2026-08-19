# Asset Upload Intake — Steps 31–33

Uploads first receive a private quarantine object key and a short expiration,
then move through validation/moderation before any asset/version/thumbnail can
be public. Browser clients receive no storage key and no public delivery URL.

The next work item is an authenticated upload endpoint plus streaming size/type
validation and a separate validation worker; it must not trust the uploaded
Content-Type header alone.
