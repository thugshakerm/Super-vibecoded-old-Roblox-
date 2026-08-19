# Launch Ticket Consumption — Step 14

Launch ticket records now support a one-time consume transition. A future game
server must submit both ticket ID and nonce to the private server-side boundary.
The ticket is valid only when the nonce digest matches, it has not expired, and
it has not been consumed.

The EF persistence implementation uses one conditional database UPDATE, so two
server processes cannot both consume the same valid ticket.
