FROM nginx:1.19.2

RUN apt update && apt install apache2-utils -y

ARG authUsername
ARG authPassword
RUN mkdir /etc/apache2
RUN htpasswd -c -b /etc/apache2/.htpasswd ${authUsername} ${authPassword}

COPY nginx/nginx.conf /etc/nginx/nginx.conf