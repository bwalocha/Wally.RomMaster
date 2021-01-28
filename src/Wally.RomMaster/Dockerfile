ARG PROJECT_NAME

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.4-buster-slim AS base
ARG PROJECT_NAME
ENV PROJECT_NAME ${PROJECT_NAME}
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-buster AS build
ARG PROJECT_NAME
RUN dotnet tool install dotnet-reportgenerator-globaltool --version 4.3.9 --tool-path /tools
WORKDIR /src
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
RUN dotnet restore "${PROJECT_NAME}/${PROJECT_NAME}.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS test
WORKDIR /src
ENTRYPOINT dotnet test \
	--test-adapter-path:. \
	--logger:'junit;LogFilePath=/mnt/artifacts/{framework}-{assembly}-test-result.xml;MethodFormat=Full;FailureBodyFormat=Verbose' \
	/p:CollectCoverage=true \
	/p:CoverletOutputFormat=cobertura \
	/p:CoverletOutput=/mnt/artifacts/coverage/ \
	&& \
	/tools/reportgenerator \
	-reports:/mnt/artifacts/coverage/coverage.cobertura.xml \
	-targetdir:/mnt/artifacts/coverage/reports \
	-reporttypes:'HTMLInline;HTMLChart'

FROM build AS publish
ARG PROJECT_NAME
WORKDIR /src/${PROJECT_NAME}
RUN dotnet publish "${PROJECT_NAME}.csproj" -c Release -o /app/publish

FROM base AS final
ARG PROJECT_NAME
ENV PROJECT_NAME ${PROJECT_NAME}
COPY --from=publish /app/publish .
ENTRYPOINT dotnet ${PROJECT_NAME}.dll