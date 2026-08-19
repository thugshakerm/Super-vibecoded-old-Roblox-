@echo off
setlocal EnableExtensions
cd /d "%~dp0"

echo.
echo === ROBLOX Revival local launcher ===
echo.

where docker >nul 2>nul
if errorlevel 1 (
  if /I "%USE_EXISTING_POSTGRES%"=="1" (
    echo Using existing native PostgreSQL on port configured in .env...
  ) else (
    echo ERROR: Docker was not found.
    echo Install/start a Docker-compatible runtime, or start native PostgreSQL and run:
    echo   set USE_EXISTING_POSTGRES=1 ^&^& run.bat
    exit /b 1
  )
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
  if "%%A"=="POSTGRES_DB" set "POSTGRES_DB=%%B"
  if "%%A"=="POSTGRES_USER" set "POSTGRES_USER=%%B"
  if "%%A"=="POSTGRES_PASSWORD" set "POSTGRES_PASSWORD=%%B"
  if "%%A"=="POSTGRES_PORT" set "POSTGRES_PORT=%%B"
)
if "%POSTGRES_DB%"=="" set "POSTGRES_DB=roblox_revival"
if "%POSTGRES_USER%"=="" set "POSTGRES_USER=roblox"
if "%POSTGRES_PASSWORD%"=="" set "POSTGRES_PASSWORD=change-this-local-password"
if "%POSTGRES_PORT%"=="" set "POSTGRES_PORT=5433"
set "ConnectionStrings__Roblox=Host=localhost;Port=%POSTGRES_PORT%;Database=%POSTGRES_DB%;Username=%POSTGRES_USER%;Password=%POSTGRES_PASSWORD%"

if /I "%USE_EXISTING_POSTGRES%"=="1" (
  echo Using existing PostgreSQL service on port %POSTGRES_PORT%...
) else (
  echo Starting PostgreSQL...
  docker compose up -d postgres
  if errorlevel 1 (
    echo ERROR: PostgreSQL could not be started.
    exit /b 1
  )
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
