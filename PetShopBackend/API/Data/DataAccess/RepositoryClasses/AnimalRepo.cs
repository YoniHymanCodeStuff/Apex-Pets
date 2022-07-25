using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DTOs;
using API.Data.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static API.utilities.utils;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class AnimalRepo : Repository<Animal>, IAnimalRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AnimalRepo(DataContext context, IMapper mapper ) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Animal> GetAnimalEagerAsync(int id)
        {
            return await _context.Animals
            //.AsNoTracking() //this could screw me over. might need 2 versions. 
            .Include(x=>x.images)
            .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {

            return await _context.Animals.Include(x=>x.images).Select(x=>x.Category).Distinct().ToListAsync();

        }

        public async Task<IEnumerable<Animal>> GetCategoryAnimalsAsync(string category){

            return await _context.Animals
            .AsNoTracking()
            .Include(x=>x.images)
            .Where(x=>x.Category == category)
            .ToListAsync();
            
        }

        
        public async Task<IEnumerable<Animal>> GetAnimalsForCheckout(Customer customer)
        {
            var IDList = new List<int>();

            foreach (var cartItem in customer.ShoppingCart)
            {
                IDList.Add(cartItem.OrderedAnimalId);
            }

            return await _context.Animals.Where(x=>IDList.Contains(x.Id)).ToListAsync();
        }
        
        public async Task<IEnumerable<CartAnimalDto>> GetCartAnimals(string username){

            var cart = await _context.Customers.Where(x=>x.UserName == username)
            .Select(x=>x.ShoppingCart).SingleOrDefaultAsync();

            var relevantIds = cart.Select(x=>x.OrderedAnimalId);
          
            var animalBank = await _context.Animals
            .Where(x=>relevantIds.Contains(x.Id)).ToListAsync();

            var cartAnimals = new List<CartAnimalDto>();

            foreach (var cartItem in cart)
            {
                var animal = animalBank.SingleOrDefault(x=>x.Id == cartItem.OrderedAnimalId);
                
                var cartAnimal = _mapper.Map<CartAnimalDto>(animal);

                cartAnimal.animalId = animal.Id;
                cartAnimal.cartItemId = cartItem.Id;

                cartAnimals.Add(cartAnimal);
            }

            return cartAnimals;
        }


    }
}