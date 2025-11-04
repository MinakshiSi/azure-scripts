using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SAS_Key_Generation;
using SAS_Key_Generation.Helpers;

namespace sas_key_rotation_function
{
    public class SasRotationFunction
    {
        private readonly ILogger<SasRotationFunction> _logger;
        private readonly IConfiguration _config;
        private readonly IStorageHelper _storageHelper;

        public SasRotationFunction(ILogger<SasRotationFunction> logger, IConfiguration config, IStorageHelper storageHelper)
        {
            _logger = logger;
            _config = config;
            _storageHelper = storageHelper;
        }

        [Function("SasRotationFunction")]
        public void Run([TimerTrigger("0 */6 * * * *")] TimerInfo timerInfo)
        {
            _logger.LogInformation("SAS Key Rotation triggered at: {time}", DateTime.UtcNow);

            try
            {
                string connectionString = _config["StorageConnectionString"];
                string containerName = _config["ContainerName"];

                var generator = new SASKeyGeneration(_storageHelper, connectionString, containerName);
                string sasUrl = generator.GenerateSASKey();

                _logger.LogInformation("SAS key generated for container '{containerName}': {sasUrl}",
                    containerName, sasUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SAS Key rotation failed: {message}", ex.Message);
            }
        }
    }
}
