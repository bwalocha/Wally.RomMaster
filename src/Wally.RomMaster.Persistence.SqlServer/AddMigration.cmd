REM @echo off
REM dotnet tool install --global dotnet-ef

IF "%1"=="" (
    SET /p MIGRATION_NAME="Enter migration name (i.e. Initial): "
)
ELSE (
    SET "MIGRATION_NAME=%1"
)

ECHO %MIGRATION_NAME%
dotnet ef migrations add %MIGRATION_NAME% --context ApplicationDbContext --startup-project ./../Wally.WebApi/Wally.WebApi.csproj --project ./Wally.Persistence.SqlServer.csproj --verbose