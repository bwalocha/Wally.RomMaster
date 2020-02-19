FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Wally.RomMaster/Wally.RomMaster.csproj", "Wally.RomMaster/"]
COPY ["Wally.RomMaster.Domain/Wally.RomMaster.Domain.csproj", "Wally.RomMaster.Domain/"]
COPY ["Wally.RomMaster.Database/Wally.RomMaster.Database.csproj", "Wally.RomMaster.Database/"]
COPY ["Wally.Database/Wally.Database.csproj", "Wally.Database/"]
COPY ["Wally.RomMaster.BusinessLogic/Wally.RomMaster.BusinessLogic.csproj", "Wally.RomMaster.BusinessLogic/"]
COPY ["Wally.RomMaster.DatFileParser/Wally.RomMaster.DatFileParser.csproj", "Wally.RomMaster.DatFileParser/"]
COPY ["Wally.RazorComponent.Grid/Wally.RazorComponent.Grid.csproj", "Wally.RazorComponent.Grid/"]
RUN dotnet restore "Wally.RomMaster/Wally.RomMaster.csproj"
COPY . .

# TEST
LABEL test=true
# install the report generator tool
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.3.9 --tool-path /tools
# run the test and collect code coverage (requires coverlet.msbuild to be added to test project)
# for exclude, use %2c for ,
RUN dotnet test --test-adapter-path:. --logger:"junit;LogFilePath=/testresults/{framework}-{assembly}-test-result.xml;MethodFormat=Full;FailureBodyFormat=Verbose" /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/testresults/coverage/
# generate html reports using report generator tool
RUN /tools/reportgenerator "-reports:/testresults/coverage/coverage.cobertura.xml" "-targetdir:/testresults/coverage/reports" "-reporttypes:HTMLInline;HTMLChart"

WORKDIR "/src/Wally.RomMaster"
RUN dotnet build "Wally.RomMaster.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wally.RomMaster.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wally.RomMaster.dll"]