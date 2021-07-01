
# HAProxy load balancer setup

* https://github.com/haproxytech/haproxy-docker-ubuntu

## Run in docker

```sh

# Build the docker image
docker build -f haproxy/Dockerfile -t hemantksingh/haproxy .

# Run haproxy on host port 80
docker run -it -p 8080:80 -p 1337:1337 hemantksingh/haproxy
```

## Troubleshooting haproxy

https://www.digitalocean.com/community/tutorials/how-to-troubleshoot-common-haproxy-errors