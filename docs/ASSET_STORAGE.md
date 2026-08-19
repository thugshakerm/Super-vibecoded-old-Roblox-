# Asset Storage Foundation — Step 7

Original uploads and rendered output are not PostgreSQL blobs. The current
`IPrivateObjectStorage` contract has no public-URL method, preventing browser
code from learning storage keys. The local development implementation stores
objects under `Storage:RootPath` (or a private application directory).

Step 7 does not publish upload/download endpoints. Production storage will use
an S3-compatible private bucket and signed/CDN delivery only after the
asset-authorization step is reviewed.
