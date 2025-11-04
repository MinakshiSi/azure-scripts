using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;

namespace SAS_Key_Generation.Helpers
{
    public  class StorageHelper : IStorageHelper
    {
        /// <summary>
        /// Creates a BlobServiceClient from the given connection string.
        /// </summary>
        public  BlobServiceClient GetBlobServiceClient(string connectionString)
        {
            return new BlobServiceClient(connectionString);
        }

        /// <summary>
        /// Generates a SAS token for a given container.
        /// </summary>
        public string GenerateContainerSasToken(string connectionString, string containerName, int validHours = 24)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerName,
                Resource = "c",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(validHours)
            };

            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.List);

            var credential = new StorageSharedKeyCredential(
                blobServiceClient.AccountName,
                GetAccountKeyFromConnectionString(connectionString)
            );

            string sasToken = sasBuilder.ToSasQueryParameters(credential).ToString();
            return $"{containerClient.Uri}?{sasToken}";
        }

        /// <summary>
        /// Extracts the account key from a connection string.
        /// </summary>
        private static string GetAccountKeyFromConnectionString(string connectionString)
        {
            var parts = connectionString.Split(';');
            foreach (var part in parts)
            {
                if (part.StartsWith("AccountKey=", StringComparison.OrdinalIgnoreCase))
                {
                    return part.Substring("AccountKey=".Length);
                }
            }

            throw new InvalidOperationException("AccountKey not found in connection string.");
        }
    }
}
