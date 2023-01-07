# https://hub.docker.com/_/nginx/tags
FROM nginx:1.23.3-alpine-slim
LABEL maintainer="wally"
ARG PROJECT_NAME \
    VERSION

EXPOSE 80
EXPOSE 443

#COPY ./src/${PROJECT_NAME}/nginx.conf /etc/nginx/conf.d/default.conf
COPY ./src/${PROJECT_NAME}/dist /usr/share/nginx/html
