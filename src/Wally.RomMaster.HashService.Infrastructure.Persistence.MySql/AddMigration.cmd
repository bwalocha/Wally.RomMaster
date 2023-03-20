REM @echo off
REM dotnet tool install --global dotnet-ef

IF "%1"=="" (
    SET /p MIGRATION_NAME="Enter migration name (i.e. Initial): "
)
ELSE (
    SET "MIGRATION_NAME=%1"
)

ECHO %MIGRATION_NAME%

SET "STARTUP_PROJECT=./../Wally.RomMaster.HashService.WebApi/Wally.RomMaster.HashService.WebApi.csproj"
SET "PROJECT=./Wally.RomMaster.HashService.Persistence.MySql.csproj"
SET "Database__ProviderType=MySql"

dotnet ef migrations add %MIGRATION_NAME% --context ApplicationDbContext --startup-project %STARTUP_PROJECT% --project %PROJECT% --verbose
