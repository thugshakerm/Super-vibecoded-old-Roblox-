# Render Worker Boundary — Step 12

The RCC adapter remains a separate internal boundary. Job scheduling, storage,
and typed RCC render requests exist; a worker may only lease queued jobs and
submit trusted render templates. It must not become a public API, accept Lua,
or directly serve output to browsers.

Actual worker leasing/dispatch begins only after the chosen 2013 RCC build is
validated in an isolated Windows environment.
