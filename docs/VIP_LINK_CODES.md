# VIP Server Link Codes — Step 13

The supplied PHP algorithm is useful and has been preserved as a C#-compatible
implementation in `VipServerLinkCodeGenerator`.

For a requested code length `L`, it uses:

```text
randomBytes = ceil(L / 4) * 3
base64 = Base64(randomBytes)
code = first L characters of base64
code = replace '+' with '-' and '/' with '_'
```

The raw VIP code is returned only at creation time. PostgreSQL stores a SHA-256
digest in `private_server_links`, not the reusable raw code.
