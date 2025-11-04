namespace SAS_Key_Generation.Helpers
{
    public interface IStorageHelper
    {
        string GenerateContainerSasToken(string connectionString, string containerName, int validHours = 24);
    }
}
