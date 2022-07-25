using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.DataAccess.UnitOfWork;
using API.Data.DTOs;
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


        [HttpPost("avatar")]
        public async Task<ActionResult<Photo>> AddAvatar(IFormFile file)
        {
            var username = User.GetUserName(); //using the extension method we created
            var user = _uow.customers.Find(x=>x.UserName == username).SingleOrDefault();//this needs to be a repo function. this is an abomination. 

            //like in all my code, need to somehow incorporate admin role. 

            var result = await _photoService.UploadPhotoAsync(file);  

            if (result.Error != null)
            {return BadRequest(result.Error.Message);}

            var photo = new Photo{
                PhotoUrl= result.SecureUrl.AbsoluteUri,
                Id = result.PublicId
                };

            _uow.photos.Add(photo);

            user.Avatar = photo;

            _uow.customers.Update(user);

            //need to add something here that indicates when you are replacing an existing image. 

            if (await _uow.Complete())
            {
                return CreatedAtRoute("GetCustomer",new {username = user.UserName}, photo);
            }
            //might need to make the route more relative. not sure how this route name
            //thing really works. 
            return BadRequest("Problem adding photos"); 
        

        }

    


        [HttpPost("animal-photo/{animalId}")]
        public async Task<ActionResult<Photo>> AddAnimalPhoto(int animalId,IFormFile file)
        {
            
            Animal animal = await _uow.animals.GetAnimalEagerAsync(animalId);
            
            var result = await _photoService.UploadPhotoAsync(file);  

            if (result.Error != null)
            {return BadRequest(result.Error.Message);}

            //need to figure out where I can add a description here. 
            //although it's just images of animals, so  myabe I dont need that at all. 

            var photo = new Photo{
                PhotoUrl= result.SecureUrl.AbsoluteUri,
                Id = result.PublicId,
                
                };

            _uow.photos.Add(photo);

            animal.images.Add(photo);

             _uow.animals.Update(animal);
            //need to add something here that indicates when you are replacing an existing image. 

            if (await _uow.Complete())
            {
                return CreatedAtRoute("GetAnimal",new {id = animal.Id}, photo);
            }
      
      
            return BadRequest("Problem adding photos"); 
        

        }

        [HttpPut("SetMainPhoto")]//I need to pass the animal, and the photoId. 
        public async Task<ActionResult> SetMainPhoto(SetPhotoDto dto)
        {
            // var username = User.GetUserName();
            // if (!await _uow.users.CheckIfIsAdminAsync(username))
            // {
            //     return BadRequest("You are not really an admin are you? maybe you should stop poking around where you don't belong... ");
            // }
            //the above should be a function. seems a shame to repeat this whole thing. not sure where I can easily reach this claims.user thing. don't get thow that works. 

            var animal = await _uow.animals.GetAnimalEagerAsync(dto.AnimalId);

            //saves unec accessing database.
            if(animal.MainPhoto != null && dto.photoId == animal.MainPhoto.Id)
            {
                return BadRequest("This photo was already main");
            }
            

            var photo = animal.images.FirstOrDefault(x=>x.Id == dto.photoId);
            
             
            animal.MainPhoto = photo;
            
             _uow.animals.Update(animal); //should be async

            if(await _uow.Complete()) return NoContent();

            return BadRequest("Failed to set photo as main");

            
        } 

        [HttpDelete("deletePhoto/{photoId}")]
        public async Task<ActionResult> DeletePhoto(string photoId)
        {
            //can I just create an inpedenpant deletion thing? 
            //Do I need to also make sure the linked entities know they have been 
            //deleted? we will see. 


            //also, need to add verfication for deleting an animal's photo. 
        
            var photo = await _uow.photos.SingleOrDefaultAsync(x=>x.Id==photoId);
            if(photo == null)
            {
                return BadRequest("photo doesn't exist in database");
            }

            var result = await _photoService.DeletePhotoAsync(photoId);

             if (result.Error != null)
            {return BadRequest(result.Error.Message);}

            

            //should be async

             _uow.photos.Remove(photo);
    
                if(await _uow.Complete())
                {
                    return Ok();
                }

            return BadRequest("failed to delete photo.");
        } 

    }

}