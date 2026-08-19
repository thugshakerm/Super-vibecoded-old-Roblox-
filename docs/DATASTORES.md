# DataStore Foundation — Step 11

DataStores are explicitly universe-scoped and server-only. Browser/website API
calls cannot satisfy `IDataStoreAccessPolicy` and must never read/write DataStore
values.

`datastore_entries` uses a unique universe/store/key tuple, a JSONB value, and a
monotonic version field. Future work must add server identity, quotas, atomic
update semantics, payload-size limits, write audit history, and retention rules
before a game-server API is exposed.
