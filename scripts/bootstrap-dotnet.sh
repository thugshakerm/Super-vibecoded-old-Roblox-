#!/usr/bin/env bash
# Installs a repository-local .NET SDK. It does not require root access and
# avoids modifying the system package manager or a developer's global SDKs.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
install_dir="${DOTNET_INSTALL_DIR:-$repo_root/.tools/dotnet}"
installer="$install_dir/dotnet-install.sh"

mkdir -p "$install_dir"

if [[ ! -x "$install_dir/dotnet" ]]; then
  echo "Downloading the official .NET installer…"
  curl --fail --location --proto '=https' --tlsv1.2 \
    https://dot.net/v1/dotnet-install.sh \
    --output "$installer"
  chmod +x "$installer"

  echo "Installing .NET SDK channel 10.0 into $install_dir…"
  "$installer" --channel 10.0 --quality GA --install-dir "$install_dir" --no-path
fi

export DOTNET_ROOT="$install_dir"
export PATH="$DOTNET_ROOT:$PATH"

"$DOTNET_ROOT/dotnet" --info
"$DOTNET_ROOT/dotnet" --list-sdks
