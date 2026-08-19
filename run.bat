@echo off
setlocal EnableExtensions
cd /d "%~dp0"

echo.
echo === ROBLOX Revival local launcher ===
echo.

where docker >nul 2>nul
if errorlevel 1 (
  echo ERROR: Docker Desktop or a Docker-compatible runtime is required for PostgreSQL.
  echo Install/start Docker Desktop, then run this file again.
  exit /b 1
)

where dotnet >nul 2>nul
if errorlevel 1 (
  echo ERROR: .NET SDK 10 is required.
  echo Install .NET 10 SDK, then run this file again.
  exit /b 1
)

if not exist ".env" (
  echo Creating .env from .env.example...
  copy /Y ".env.example" ".env" >nul
)

for /f "usebackq tokens=1,* delims==" %%A in (".env") do (
  if "%%A"=="POSTGRES_PASSWORD" set "POSTGRES_PASSWORD=%%B"
)
if "%POSTGRES_PASSWORD%"=="" set "POSTGRES_PASSWORD=change-this-local-password"
set "ConnectionStrings__Roblox=Host=localhost;Port=5432;Database=roblox_revival;Username=roblox;Password=%POSTGRES_PASSWORD%"

echo Starting PostgreSQL...
docker compose up -d postgres
if errorlevel 1 (
  echo ERROR: PostgreSQL could not be started.
  exit /b 1
)

echo Restoring solution packages...
dotnet restore Roblox.Revival.sln
if errorlevel 1 (
  echo ERROR: dotnet restore failed.
  exit /b 1
)

echo Building solution...
dotnet build Roblox.Revival.sln --no-restore
if errorlevel 1 (
  echo ERROR: dotnet build failed.
  exit /b 1
)

echo.
echo Starting API at http://localhost:5200 ...
start "ROBLOX Revival API" cmd /k "set ConnectionStrings__Roblox=%ConnectionStrings__Roblox% && dotnet run --project src\Roblox.Api --no-build"

echo Starting Website at http://localhost:5100 ...
start "ROBLOX Revival Website" cmd /k "set ConnectionStrings__Roblox=%ConnectionStrings__Roblox% && dotnet run --project src\Roblox.Website --no-build"

echo.
echo Started:
echo   Website: http://localhost:5100/health
echo   API:     http://localhost:5200/health
echo.
echo Close the two named command windows to stop the website and API.
echo Run "docker compose down" later to stop PostgreSQL.
endlocal
