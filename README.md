# Azure SAS Key Automation

This project automates Azure Storage SAS key generation and secure distribution using Azure Functions.  
It is built with Infrastructure as Code (Bicep) and automated CI/CD pipelines via GitHub Actions.

## Architecture
- Timer-triggered Azure Function
- Infrastructure as Code (Bicep)
- GitHub Actions for CI/CD
- Application Insights for monitoring

## Deployment Steps
1. Fork this repo and clone locally.
2. Add Azure credentials to GitHub Secrets:
   - `AZURE_CREDENTIALS`
   - `AZURE_RESOURCE_GROUP`
   - `FUNCTION_APP_NAME`
3. Push to `main` branch → deployment auto-triggers.

## Tech Stack
- .NET 8
- Azure Functions
- Bicep (IaC)
- GitHub Actions
- Azure Storage, App Insights

## Note
This repository contains sample code only — no production secrets or real SAS keys.
