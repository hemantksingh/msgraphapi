# Running the load tests

Load testing is done via a python based tool locust: https://locust.io/. You will need to make sure you have `pip` installed. This can be done on an ubuntu box using:

```sh
sudo apt-get update
sudo apt-get install pip -y
```

## Run locust in a virtulal env

```sh

# Install virtualenv if it is not already installed
pip install virtualenv
export PATH="$HOME/.local/bin:$PATH"

# Create a virtualenv and install locust in it
virtualenv create venv
source venv/bin/activate
pip install locust

# Run the load test
source venv/bin/activate
export MSGRAPH_USER=xxxxx
export MSGRAPH_PASSWORD=xxxxx

locust
```

You should be able to access the locust web interface at http://localhost:8089