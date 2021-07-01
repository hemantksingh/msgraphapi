# Nginx reverse proxy setup

## Run in docker

```sh

# Build the docker image
docker build -f Dockerfile.nginx -t hemantksingh/nginx .

# Run nginx on host port 80
docker run -it -p 80:80 hemantksingh/nginx

```
