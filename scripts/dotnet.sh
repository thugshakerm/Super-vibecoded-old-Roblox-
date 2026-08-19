#!/usr/bin/env bash
# Runs the repository-local SDK installed by scripts/bootstrap-dotnet.sh.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
dotnet_root="${DOTNET_INSTALL_DIR:-$repo_root/.tools/dotnet}"

if [[ ! -x "$dotnet_root/dotnet" ]]; then
  echo "Repository-local .NET SDK is not installed." >&2
  echo "Run: ./scripts/bootstrap-dotnet.sh" >&2
  exit 1
fi

export DOTNET_ROOT="$dotnet_root"
export PATH="$DOTNET_ROOT:$PATH"
exec "$DOTNET_ROOT/dotnet" "$@"
