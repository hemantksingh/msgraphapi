
# Setup

* https://github.com/haproxytech/haproxy-docker-ubuntu

## Run in docker

```sh

# Build the docker image
docker build -f Dockerfile.haproxy -t hemantksingh/haproxy .

# Run on host port 4000 
docker run -it -p 8080:4000 hemantksingh/haproxy
```
