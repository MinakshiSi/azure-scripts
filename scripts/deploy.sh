#!/bin/bash
RESOURCE_GROUP="SASKeyRG"
DEPLOYMENT_NAME="sas-deploy-dev"
PARAM_FILE="./infrastructure/parameters/dev.parameters.json"

az deployment group create \
  --name $DEPLOYMENT_NAME \
  --resource-group $RESOURCE_GROUP \
  --template-file ./infrastructure/main.bicep \
  --parameters @$PARAM_FILE
