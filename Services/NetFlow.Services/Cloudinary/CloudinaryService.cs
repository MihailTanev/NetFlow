﻿namespace NetFlow.Services.Cloudinary
{
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadCoursePictureAsync(IFormFile coursePicture, string fileName)
        {
            byte[] content = null;

            using (var ms = new MemoryStream())
            {
                await coursePicture.CopyToAsync(ms);
                content = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(content))
            {
                ImageUploadParams imageUploadParams = new ImageUploadParams
                {
                    Folder = "NetFlow/CourseImages",
                    File = new FileDescription(fileName, ms)
                };

                uploadResult = this.cloudinary.Upload(imageUploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }

        public async Task<string> UploadPostPictureAsync(IFormFile postImage, string fileName)
        {
            byte[] content = null;

            using (var ms = new MemoryStream())
            {
                await postImage.CopyToAsync(ms);
                content = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(content))
            {
                ImageUploadParams imageUploadParams = new ImageUploadParams
                {
                    Folder = "NetFlow/PostImages",
                    File = new FileDescription(fileName, ms)
                };

                uploadResult = this.cloudinary.Upload(imageUploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }

        public async Task<string> UploadProfilePictureAsync(IFormFile profileImage, string fileName)
        {
            byte[] content = null;

            using (var ms = new MemoryStream())
            {
                await profileImage.CopyToAsync(ms);
                content = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(content))
            {
                ImageUploadParams imageUploadParams = new ImageUploadParams
                {
                    Folder = "NetFlow/ProfileImages",
                    File = new FileDescription(fileName, ms)
                };

                uploadResult = this.cloudinary.Upload(imageUploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}