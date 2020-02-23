FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
EXPOSE 80
EXPOSE 443

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
WORKDIR "/src/Wally.RomMaster"
RUN dotnet build "Wally.RomMaster.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src"
RUN echo "echo empty:" > test.sh
RUN echo "ls -al /artifacts" >> test.sh
RUN echo "dotnet test --test-adapter-path:. --logger:'junit;LogFilePath=/artifacts/{framework}-{assembly}-test-result.xml;MethodFormat=Full;FailureBodyFormat=Verbose' /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=/artifacts/coverage/" >> test.sh
RUN echo "/tools/reportgenerator -reports:/artifacts/coverage/coverage.cobertura.xml -targetdir:/artifacts/coverage/reports -reporttypes:'HTMLInline;HTMLChart'" >> test.sh
RUN echo "echo a:" >> test.sh
RUN echo "ls -al /artifacts" >> test.sh
RUN echo "touch /artifacts/2" >> test.sh
RUN echo "echo t:" >> test.sh
RUN echo "ls -al /artifacts" >> test.sh
ENTRYPOINT ["sh", "test.sh"]

FROM build AS publish
RUN dotnet publish "Wally.RomMaster.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wally.RomMaster.dll"]