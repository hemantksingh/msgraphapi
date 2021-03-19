# MS Graph API

Provides integration with MS graph API using the client credentials flow for a [trusted background application](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-daemon-overview), that does not require a signed-in user for access to the MS Graph API.

## Tools for interacting with MS Graph API

Graph Explorer is a web-based tool that you can use to build and test requests using Microsoft Graph APIs. You can access Graph Explorer at: `https://developer.microsoft.com/en-us/graph/graph-explore`

## Build

```sh
# build with docker
docker build -t hemantksingh/msgraphapi .
docker build -t hemantksingh/msgraphapi-rproxy .

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

# run with docker behind an nginx reverse proxy on: http://localhost/swagger
docker-compose up

# run with dotnet standalone on: http://localhost:4000/swagger
dtonet run
```
