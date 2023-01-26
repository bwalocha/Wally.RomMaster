# https://hub.docker.com/_/microsoft-dotnet-aspnet
# FROM mcr.microsoft.com/dotnet/aspnet:7.0.0-alpine3.16
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine-amd64
LABEL maintainer="wally"
ARG PROJECT_NAME

#EXPOSE 80
#EXPOSE 443

# https://www.abhith.net/blog/docker-sql-error-on-aspnet-core-alpine/
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
#ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED true
ENV PROJECT_NAME ${PROJECT_NAME}

#RUN apk add icu-libs --no-cache && \
#    apk add libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/ && \
#    apk add msttcorefonts-installer fontconfig --no-cache && \
#    update-ms-fonts && fc-cache -f

WORKDIR /app
COPY ./packages/${PROJECT_NAME} .

ENTRYPOINT dotnet ${PROJECT_NAME}.dll

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
# ENV \
#     DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#     LC_ALL=en_US.UTF-8 \
#     LANG=en_US.UTF-8
# RUN apk add --no-cache \
#     icu-data-full \
#     icu-libs
