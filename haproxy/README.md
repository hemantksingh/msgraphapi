
# Setup

* https://github.com/haproxytech/haproxy-docker-ubuntu

## Run in docker

```sh

# Build the docker image
docker build -f Dockerfile.haproxy -t hemantksingh/haproxy .

# Run haproxy on host port 80
docker run -it -p 8080:80 -p 1337:1337 hemantksingh/haproxy
```

You should be able to access the

* haproxy `stats` page on the host at http://localhost:1337
* api at http://localhost/azuread/domains