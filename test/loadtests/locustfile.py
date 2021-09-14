from locust import HttpUser, task
import os

class MsGraphUser(HttpUser):
    
    def auth_creds(self):
        return (os.environ['MSGRAPH_USER'], os.environ['MSGRAPH_PASSWORD'])
    
    @task
    def domains(self):
        self.client.get("/azuread/users", auth=self.auth_creds())
    
    @task
    def foo(self):
        self.client.get("/azuread/groups", auth=self.auth_creds())