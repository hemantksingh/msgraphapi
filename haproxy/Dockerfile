FROM haproxytech/haproxy-ubuntu:2.0
COPY haproxy/haproxy.cfg /usr/local/etc/haproxy/haproxy.cfg
COPY haproxy/rates.map /etc/haproxy/maps/rates.map
COPY haproxy/dataplaneapi.hcl /etc/haproxy/dataplaneapi.hcl

ADD https://github.com/haproxytech/dataplaneapi/releases/download/v2.3.3/dataplaneapi_2.3.3_Linux_x86_64.tar.gz /tmp

RUN ls -al /tmp
RUN tar -zxvf /tmp/dataplaneapi_2.3.3_Linux_x86_64.tar.gz
RUN chmod +x build/dataplaneapi
# RUN ls -al build
# COPY build/dataplaneapi/ /usr/local/bin/