
# HAProxy load balancer setup

* https://github.com/haproxytech/haproxy-docker-ubuntu

## Run in docker

```sh

# Build the docker image
docker build -f haproxy/Dockerfile -t hemantksingh/haproxy .

# Run haproxy on host port 80
docker run -it -p 8080:80 -p 1337:1337 -p 5555:5555 hemantksingh/haproxy
```

## Dataplane API

You can access the [HAProxy Dataplane API](https://www.haproxy.com/blog/new-haproxy-data-plane-api/) on port `5555`

```sh

curl --get --user admin:adminpwd \
    http://localhost:5555/v2/services/haproxy/configuration/backends

```

Further API usage can be found on the official docs: https://www.haproxy.com/documentation/hapee/latest/api/data-plane-api/usage/

## Troubleshooting haproxy

https://www.digitalocean.com/community/tutorials/how-to-troubleshoot-common-haproxy-errors