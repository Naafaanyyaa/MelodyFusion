using Azure.Storage;
using Azure.Storage.Blobs;
using MelodyFusion.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MelodyFusion.BLL.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AzureBlobService> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobService(IConfiguration config, ILogger<AzureBlobService> logger)
        {
            _config = config;
            _logger = logger;

            var credential = new StorageSharedKeyCredential(config.GetValue<string>("AzureBlob:StorageAccount"), config.GetValue<string>("AzureBlob:Key"));
            var blobUri = $"https://{config.GetValue<string>("AzureBlob:StorageAccount")}.blob.core.windows.net";
            _blobServiceClient = new BlobServiceClient(new Uri(blobUri), credential);
        }

        public async Task<List<string>> ListBlobContainerNamesAsync()
        {
            var containers= _blobServiceClient.GetBlobContainersAsync();

            var containerNames = new List<string>();

            await foreach (var container in containers)
            {
                containerNames.Add(container.Name);
            }

            return containerNames;
        }

        public async Task<IEnumerable<Uri>> SavePhotoAsync(IFormFileCollection files, string containerName, string directoryToSave)
        {
            if (!files.Any())
            {
                return Enumerable.Empty<Uri>();
            }

            var blobUriList = new List<Uri>();

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            foreach (var file in files!)
            {
                var blobClient = blobContainerClient.GetBlobClient(Path.Combine(directoryToSave, DateTime.UtcNow.ToString(), file.FileName));

                await blobClient.UploadAsync(file.OpenReadStream());

                blobUriList.Add(blobClient.Uri);
            }

            return blobUriList;
        }
    }
}
