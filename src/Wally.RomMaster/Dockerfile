FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.2-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.103-buster AS build
# install the report generator tool
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.3.9 --tool-path /tools

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
RUN dotnet build -c Release -o /app/build

FROM build AS test
WORKDIR /src
RUN echo "dotnet test --test-adapter-path:. --logger:'junit;LogFilePath=/mnt/artifacts/{framework}-{assembly}-test-result.xml;MethodFormat=Full;FailureBodyFormat=Verbose' /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/mnt/artifacts/coverage/" >> test.sh
RUN echo "/tools/reportgenerator -reports:/mnt/artifacts/coverage/coverage.cobertura.xml -targetdir:/mnt/artifacts/coverage/reports -reporttypes:'HTMLInline;HTMLChart'" >> test.sh
ENTRYPOINT ["sh", "test.sh"]

FROM build AS publish
WORKDIR /src/Wally.RomMaster
RUN dotnet publish "Wally.RomMaster.csproj" -c Release -o /app/publish

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wally.RomMaster.dll"]