using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SAS_Key_Generation; // reference to your class library
using SAS_Key_Generation.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace sas_key_generation_service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStorageHelper _storageHelper;
        private Timer _timer;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IStorageHelper storageHelper)
        {
            _logger = logger;
            _configuration = configuration;
            _storageHelper = storageHelper;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // run immediately, then every 5 minutes
            _timer = new Timer(ExecuteJob, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            _logger.LogInformation("SAS Key Generation Worker started at: {time}", DateTimeOffset.Now);
            return Task.CompletedTask;
        }

        private void ExecuteJob(object state)
        {
            try
            {
                string connectionString = _configuration["StorageConnectionString"];
                string containerName = _configuration["ContainerName"];

                _logger.LogInformation("SAS Key generation job started at {time}", DateTimeOffset.Now);

                var generator = new SASKeyGeneration(_storageHelper,connectionString, containerName);
                string sasUrl = generator.GenerateSASKey();

                _logger.LogInformation("Generated SAS URL: {sasUrl}", sasUrl);
                _logger.LogInformation("SAS Key generation job completed at {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during SAS Key generation job.");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            _logger.LogInformation("SAS Key Generation Worker stopped at: {time}", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }
    }
}
