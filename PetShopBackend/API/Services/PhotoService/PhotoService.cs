using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.utilities;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace API.Services.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }

//need to make a version of this that saves the correct type for animal pics. 
//currently the one I have is good for avatars. 
        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
        {
           var uploadResult = new ImageUploadResult();
          
           if(file.Length>0){
            using var stream = file.OpenReadStream();//opening a new stream to read the binary file
            var uploadParams = new ImageUploadParams(){
                File = new FileDescription(file.FileName,stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")//this is how I want to edit the photo
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);
           }
            return uploadResult;
        }
    }
}