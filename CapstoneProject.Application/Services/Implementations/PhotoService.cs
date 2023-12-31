﻿using CapstoneProject.Application.Services.Abstractions;
using CapstoneProject.Shared.CloudinaryProperties;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CapstoneProject.Application.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        public IConfiguration Configuration { get; }
        private CloudinarySettings _cloudinarySettings;
        private Cloudinary _cloudinary;
        public PhotoService(IConfiguration configuration)
        {
            Configuration = configuration;
            _cloudinarySettings = new();
            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            _cloudinarySettings.CloudName = cloudinarySettings["CloudName"];
            _cloudinarySettings.ApiKey = cloudinarySettings["ApiKey"];
            _cloudinarySettings.ApiSecret = cloudinarySettings["ApiSecret"];
            Account account = new Account(_cloudinarySettings.CloudName, _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public string AddPhoto(IFormFile file)
        {
            var uploadResultParams = new ImageUploadResult();
            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResultParams = _cloudinary.Upload(uploadParams);
                }
            }
            string url = uploadResultParams.Url.ToString();
            string publicId = uploadResultParams.PublicId;

            return url;
        }
    }
}
