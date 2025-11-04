using SAS_Key_Generation.Helpers;
using System;

namespace SAS_Key_Generation
{
    public class SASKeyGeneration
    {
        private readonly IStorageHelper _storageHelper;
        private readonly string _connectionString;
        private readonly string _containerName;

        public SASKeyGeneration(IStorageHelper storageHelper, string connectionString, string containerName)
        {
            _storageHelper = storageHelper ?? throw new ArgumentNullException(nameof(storageHelper));
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _containerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
        }

        public string GenerateSASKey()
        {
            try
            {
                return _storageHelper.GenerateContainerSasToken(_connectionString, _containerName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error generating SAS key: {ex.Message}", ex);
            }
        }
    }
}
