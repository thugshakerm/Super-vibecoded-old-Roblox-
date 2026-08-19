# Asset Upload Intake API — Step 34

`POST /api/assets/uploads/intake` requires an authenticated opaque session and
creates only a private, expiring upload-intake record. It returns an upload ID,
expiry, and maximum size; it never returns an object-storage key or public URL.

The next step will add the actual streaming file transfer, ownership/expiry
checks, and state transition from Created to Uploaded.
