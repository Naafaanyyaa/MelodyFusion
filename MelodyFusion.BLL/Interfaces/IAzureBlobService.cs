using Microsoft.AspNetCore.Http;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IAzureBlobService
    {
        Task<List<string>> ListBlobContainerNamesAsync();
        Task<IEnumerable<Uri>> SavePhotoAsync(IFormFileCollection files, string containerName, string directoryToSave);
    }
}