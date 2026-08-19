# .NET SDK Setup

The project targets .NET 10 and pins SDK selection in `global.json`. To make
setup reproducible without modifying a workstation's system .NET installation,
use the repository-local bootstrap script:

```bash
./scripts/bootstrap-dotnet.sh
```

It installs the official .NET 10 GA SDK into `.tools/dotnet/`, which is ignored
by Git. Then use the wrapper for all local commands:

```bash
./scripts/dotnet.sh restore Roblox.Revival.sln
./scripts/dotnet.sh build Roblox.Revival.sln --no-restore
./scripts/dotnet.sh test Roblox.Revival.sln --no-build
```

The script needs outbound HTTPS access to `https://dot.net` and the official
.NET artifact host. It does not use `sudo`, apt repositories, or a global SDK.

## Sandbox result

This development sandbox currently has no `dotnet` executable. The Debian APT
sources and direct HTTPS downloads are blocked by its outbound network policy,
so installation cannot complete here until that egress is available. The
repository-local setup is ready to run immediately in an environment with
normal HTTPS access.
