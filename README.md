# Azure SAS Key Automation

This project automates the generation and distribution of Azure Storage Service SAS Keys using an Azure Function.
The solution is fully automated with Infrastructure as Code (Bicep) and deployed using GitHub Actions.

## Features
- Timer-triggered Azure Function for SAS key generation
- Secure distribution via managed identity
- Full IaC deployment using Bicep
- Automated CI/CD via GitHub Actions

## Folder Structure
See `/src` for function code, `/infrastructure` for Bicep templates, and `.github/workflows` for pipeline definition.

## Deployment
1. Create an Azure Service Principal and add it as `AZURE_CREDENTIALS` in GitHub Secrets.
2. Update `resourceGroupName` in `build-and-deploy.yml`.
3. Push to `main` — CI/CD pipeline will build, deploy infrastructure, and publish the Function.

## Tech Stack
- .NET 8
- Azure Functions
- Bicep (IaC)
- Azure DevOps / GitHub Actions
