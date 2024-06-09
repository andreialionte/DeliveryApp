namespace DeliveryApp.API.Services
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile file, string fileKey);
    }
}

