ARG PROJECT_NAME

# https://hub.docker.com/_/microsoft-dotnet-aspnet
FROM mcr.microsoft.com/dotnet/aspnet:5.0.3-alpine3.13-amd64 AS base
ARG PROJECT_NAME
ENV PROJECT_NAME ${PROJECT_NAME}
WORKDIR /app
#EXPOSE 80
EXPOSE 443

# https://hub.docker.com/_/microsoft-dotnet-sdk
FROM mcr.microsoft.com/dotnet/sdk:5.0.103-alpine3.13-amd64 AS build
ARG PROJECT_NAME
# https://www.nuget.org/packages/dotnet-reportgenerator-globaltool/
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.8.5 --tool-path /tools
WORKDIR /src
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore
COPY . .
RUN dotnet build -c Release /p:CONTINUOUS_INTEGRATION=true --no-restore

FROM build AS test
WORKDIR /src
ENTRYPOINT dotnet test \
	--no-restore \
	--test-adapter-path:. \
	--logger:"junit;LogFilePath=/mnt/artifacts/{framework}-{assembly}-test-result.xml;MethodFormat=Full;FailureBodyFormat=Verbose" \
	/p:CONTINUOUS_INTEGRATION=true \
	/p:CollectCoverage=true \
	/p:CoverletOutputFormat=cobertura \
	/p:CoverletOutput=/mnt/artifacts/coverage/ \
	&& \
	/tools/reportgenerator \
	-reports:/mnt/artifacts/coverage/coverage.cobertura.xml \
	-targetdir:/mnt/artifacts/coverage/reports \
	-reporttypes:"HTMLInline;HTMLChart"

FROM build AS publish
ARG PROJECT_NAME
WORKDIR /src/${PROJECT_NAME}
RUN dotnet publish "${PROJECT_NAME}.csproj" -c Release -o /app/publish --no-restore --no-build

FROM base AS final
ARG PROJECT_NAME
ENV PROJECT_NAME ${PROJECT_NAME}
COPY --from=publish /app/publish .
ENTRYPOINT dotnet ${PROJECT_NAME}.dll
