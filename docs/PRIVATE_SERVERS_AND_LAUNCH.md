# Private Servers and Launch Tickets — Step 10

Step 10 establishes persistent private-server ownership/access records and
single-use launch-ticket storage. It does not allocate running game servers or
expose a launch endpoint.

A launch ticket stores only a SHA-256 nonce digest. Its raw nonce is generated
cryptographically and is reserved for the future trusted launcher-to-game-server
handoff. Browser code must not be given server internals or arbitrary join data.
