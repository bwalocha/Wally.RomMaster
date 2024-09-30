REM @echo off
REM dotnet tool install --global dotnet-ef

IF "%1"=="" (
    SET /p MIGRATION_NAME="Enter migration name (i.e. Initial): "
)
ELSE (
    SET "MIGRATION_NAME=%1"
)

ECHO %MIGRATION_NAME%

SET "STARTUP_PROJECT=./../Wally.RomMaster.NotificationService.WebApi/Wally.RomMaster.NotificationService.WebApi.csproj"
SET "PROJECT=./Wally.RomMaster.NotificationService.Infrastructure.Persistence.SqlServer.csproj"
SET "Database__ProviderType=SqlServer"

dotnet ef migrations add %MIGRATION_NAME% --context ApplicationDbContext --startup-project %STARTUP_PROJECT% --project %PROJECT% --verbose
