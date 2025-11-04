@description('Azure region for deployment')
param location string = resourceGroup().location

@description('Prefix for resource naming')
param prefix string = 'saskey'

@description('Environment name (dev, prod)')
param environment string = 'dev'

@description('Storage account name')
param storageAccountName string = take(toLower('${prefix}${environment}${uniqueString(resourceGroup().id)}'), 24)

@description('Function App name')
param functionAppName string = '${prefix}func${uniqueString(resourceGroup().id)}'

@description('App Insights name')
param appInsightsName string = '${prefix}ai${uniqueString(resourceGroup().id)}'

@description('SKU for Function App Plan')
param planSku string = 'Y1' // Y1 = Consumption

// ─── Storage Module ───────────────────────────────
module storage './storage.bicep' = {
  name: 'storageDeployment'
  params: {
    storageAccountName: storageAccountName
    location: location
  }
}

// ─── Application Insights Module ─────────────────
module insights './appinsights.bicep' = {
  name: 'appInsightsDeployment'
  params: {
    appInsightsName: appInsightsName
    location: location
  }
}

// ─── Function App Module ─────────────────────────
module functionApp './functionapp.bicep' = {
  name: 'functionAppDeployment'
  params: {
    location: location
    functionAppName: functionAppName
    storageAccountName: storage.outputs.storageAccountName
    appInsightsInstrumentationKey: insights.outputs.instrumentationKey
    planSku: planSku
  }
}

output functionAppUrl string = functionApp.outputs.defaultHostName
