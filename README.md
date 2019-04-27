# Wally.RomMaster

ROM Master helps to manage collections of games supported by an emulator

## Requirements

- [ ] .NET Core 3.0 (at least [3.0.100-preview4-011223](https://dotnet.microsoft.com/download/dotnet-core/3.0))

## Installation

```bash
dotnet build
dotnet run --project Wally.RomMaster/Wally.RomMaster.csproj
```

## Test

## Run

Go to the [https://localhost:5001](https://localhost:5001)

## Queries

### Show Files of the Dats

```sql
SELECT *
FROM File f LEFT JOIN Dat d ON d.FileId = f.Id
WHERE 
f.Path NOT LIKE '%.bak'
AND f.Path NOT LIKE '%.txt'
--AND d.Id IS NULL
ORDER BY f.Path
```

### Show ROM Files in {PATH} location:`

```sql
SELECT *
FROM File f
JOIN Rom r ON f.Crc = r.Crc
WHERE f.Path LIKE '{PATH:\\NAS\WD8A\emu\ToSort%}'
```

### Show Game name and File path of {FAT NAME} DAT file data set

```sql
SELECT g.name, f.path
FROM Dat d
JOIN Game g ON d.id = g.datid
JOIN Rom r ON r.gameid = g.id
JOIN File f ON f.crc = r.crc AND f.size = r.size
WHERE d.name = '{DAT NAME}'
ORDER BY f.path
```

## Resources

https://learn-blazor.com/
https://www.youtube.com/watch?v=GI_9g9lZpik
https://blazor.net/docs/get-started.html
https://blazor-demo.github.io/

https://blogs.msdn.microsoft.com/webdev/2018/03/22/get-started-building-net-web-apps-in-the-browser-with-blazor/
https://blogs.msdn.microsoft.com/webdev/2018/02/06/blazor-experimental-project/
https://github.com/aspnet/blazor

https://github.com/BlazorExtensions/SignalR
https://www.nuget.org/packages/Blazor.Extensions.SignalR
https://github.com/csnewman/BlazorSignalR
https://github.com/conficient/BlazorChatSample

https://github.com/Mewriick/Blazor.FlexGrid
https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/angular?view=aspnetcore-2.1&tabs=netcore-cli

## ToDo

- [ ] To add DatFile page
- [ ] To add Debug page with realtime Logging messages
- [ ] To add parsing `clrmamepro` DAT file support
- [ ] To process new files when new folder with files created (copied folder with files to i.e. DatRoot)
- [ ] To extend Dat model (name, description, category?, version, date?, author, email?, homepage?, url?, comment?, clrmamepro?, romcenter?
- [ ] Add Index on File.Crc, Rom.Crc
- [ ] Docker container
- [ ] GitLab CI YAML script
