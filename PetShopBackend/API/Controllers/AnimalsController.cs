using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Data.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API.Data.DTOs;
using API.helpers;
using API.Extensions;
using AutoMapper;

namespace PetShop.PetShopBackend.API.Controllers
{

    public class AnimalsController : BaseApiController
    {

        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public AnimalsController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
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

            return Ok(cates);

        }

        [HttpGet("Paginated")]
        public async Task<ActionResult<PagedList<Animal>>> GetPaginatedAnimals([FromQuery] AnimalQueryParams queryParams)
        {
            var animals = await _uow.animals.GetPagedAnimalsAsync(queryParams);

            Response.AddPaginationHeader(animals.CurrentPage, animals.Pagesize, animals.TotalItems, animals.TotalPages);

            return Ok(animals);
        }

  
        [HttpGet("{id}", Name = "GetAnimal")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {

            var animal = await _uow.animals.GetAnimalEagerAsync(id);


            return animal;


        }

        [HttpPut]
        public async Task<ActionResult> UpdateAnimal(Animal animal)
        {

            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}


            _uow.animals.Update(animal);

            if (await _uow.Complete())
            {
                return NoContent();
            }

            return BadRequest("Failed to update animal");
        }

        [HttpPost("NewAnimal")]
        public async Task<ActionResult<Animal>> AddNewAnimal(CreateAnimalDto dto)
        {
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}

            var animal = _mapper.Map<Animal>(dto);

            _uow.animals.Add(animal);

            if (await _uow.Complete())
            {
                return animal;
            }

            return BadRequest("Failed to add animal to database");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAnimal(int id)
        {
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}
            
            
            await _uow.animals.RemoveByIdAsync(id);

            if (await _uow.Complete())
            {
                return NoContent();
            }

            return BadRequest("Failed to delete product from database");
        }

        [HttpDelete("Archive/{id}")]
        public async Task<ActionResult> ArchiveAnimal(int id)
        {
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}
            
            await _uow.animals.ArchiveAnimalAsync(id);

            if (await _uow.Complete())
            {
                return NoContent();
            }

            return BadRequest("Failed to archive product");
        }

    }
}