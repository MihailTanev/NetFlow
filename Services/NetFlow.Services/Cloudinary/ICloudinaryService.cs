namespace NetFlow.Services.Cloudinary
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task<string> UploadCoursePictureAsync(IFormFile pictureFile, string fileName);

        Task<string> UploadPostPictureAsync(IFormFile postImage, string fileName);

    }
}
