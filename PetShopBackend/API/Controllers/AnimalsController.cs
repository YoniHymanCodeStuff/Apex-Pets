using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.DataAccess;
using API.Data.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using API.utilities;

namespace PetShop.PetShopBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        
        private readonly IUoW _uow;
        
        //I probably should be using the IUOW here, that 
        //is the whole point of it existing. 
        //unfortunately it is buggy for unknown reasons. 
        //maybe clarifying this mess of who is in charge and 
        //where the context should come from will help... 
        public AnimalsController(IUoW uow)
        {
            _uow = uow;
                                    
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            var animals = await _uow.animals.GetAllAsync();
            return Ok(animals);
            
        }



        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetAnimalCategories()
        {
            var cates = await _uow.animals.GetCategoriesAsync();
            
            foreach (var i in cates)
            {
                utils.DebugMsg(i);
            }     

            return Ok(cates);
            
        }

        [HttpGet("Categories/{category}")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalsByCategory(string category)
        {
                        
            var CategoryAnimals = await _uow.animals.GetCategoryAnimalsAsync(category);
             
            return Ok(CategoryAnimals);

            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
                        
            var animal = await _uow.animals.GetAsync(id);
             
            return animal;

            
        }

    }
}