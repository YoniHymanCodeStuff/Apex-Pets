using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.UnitOfWork;
using API.Data.Model;
using API.Extensions;
using API.Services.PhotoService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class PhotoController : BaseApiController
    {
        private readonly IUoW _uow;
        private readonly IPhotoService _photoService;

        public PhotoController(IUoW uow, IPhotoService photoService)
        {
            _photoService = photoService;
            this._uow = uow;
        }


        [HttpPost]
        public async Task<ActionResult<Photo>> AddPhoto(IFormFile file)
        {
            var username = User.GetUserName(); //using the extension method we created
            var user = _uow.users.Find(x=>x.UserName == username).SingleOrDefault();//this needs to be a repo function. this is an abomination. 
            var result = await _photoService.UploadPhotoAsync(file);  

            if (result.Error != null)
            {return BadRequest(result.Error.Message);}

            var photo = new Photo{
                PhotoUrl= result.SecureUrl.AbsoluteUri,
                Id = result.PublicId
                };

            user.Avatar = photo;

            //need to add something here that indicates when you are replacing an existing image. 

            if (await _uow.Complete())
            {
                return CreatedAtRoute("GetUser", new { username = user.UserName }, photo);
            }

            return BadRequest("Problem adding photos"); 
        

        }

    }
}