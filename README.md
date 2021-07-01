# MS Graph API

Provides integration with MS graph API using the client credentials flow for a [trusted background application](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-daemon-overview), that does not require a signed-in user for access to the MS Graph API.

## Tools for interacting with MS Graph API

Graph Explorer is a web-based tool that you can use to build and test requests using Microsoft Graph APIs. You can access Graph Explorer at: `https://developer.microsoft.com/en-us/graph/graph-explore`

## Building the application

```sh
# build with docker
docker build -t hemantksingh/msgraphapi .

# build with dotnet
dotnet build
```

## Running the application

Running the application requires the Azure AD [app registration](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-daemon-app-registration) details for authenticating with MS Identity platform and invoke the MS graph API with a token. The registration details can be provided by adding an `appsettings.secrets.json` file or exported as environment variables

```sh

# create an appsettings.secrets.json file
{
  "AzureAD": {
    "ClientId": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "ClientSecret": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    "TenantId": "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
  }
}


# or export the following environment variables
AZUREAD_CLIENT_ID=xxxx
AZUREAD_CLIENT_SECRET=xxxx
AZUREAD_TENANT_ID=xxxx

```

### Run with nginx reverse proxy

This runs the API behind an nginx reverse proxy which has basic authentication enabled

```sh
authPassword=<authPassword> docker compose -f docker-compose-nginx.yml up --build
```

The API can be accessed at: http://localhost/azuread/swagger

### Run with haproxy load balancer

This runs the API behind HAProxy load balancer 

```sh
docker compose -f docker-compose-haproxy.yml up --build
```

You should be able to access

* haproxy `stats` page on the host at http://localhost:1337
* prometheus metrics at http://localhost/metrics
* API at http://localhost/azuread/swagger

### Run with dotnet standalone

```sh
dtonet run
```

The API can be accessed at: http://localhost:4000/swagger
