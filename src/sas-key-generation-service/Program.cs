using SAS_Key_Generation.Helpers;
using sas_key_generation_service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IStorageHelper, StorageHelper>();

var host = builder.Build();
host.Run();
