# Economy and Limiteds Foundation — Steps 19–20

The public `ccr0/economy-simulator` source has been studied as an architectural
reference only. It exposes useful concepts: limited/limited-unique item
classification, individually owned serials, resellers, sale history, and a
recent-average-price display. It is a 2016-era revival, so it is not a visual
or exact-2013 behavior source.

This project uses an immutable currency ledger, serial-numbered limited
instances, and immutable completed sales. RAP is isolated behind `IRapCalculator`.
The current arithmetic-average implementation does not hardcode a historical
sample count/window; that policy requires explicit approved reference evidence.
