using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.Model;
using Microsoft.EntityFrameworkCore;
using static API.utilities.utils;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class AnimalRepo : Repository<Animal>, IAnimalRepo
    {
        private readonly DataContext _context;
        
        public AnimalRepo(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Animal> GetAnimalEagerAsync(int id)
        {
            return await _context.Animals
            .Include(x=>x.images)
            .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {

            return await _context.Animals.Include(x=>x.images).Select(x=>x.Category).Distinct().ToListAsync();

        }

        public async Task<IEnumerable<Animal>> GetCategoryAnimalsAsync(string category){

            return await _context.Animals.Include(x=>x.images).Where(x=>x.Category == category).ToListAsync();
            
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
        
        public async Task<IEnumerable<Animal>> GetCartAnimals(string username){

            var cart = await _context.Customers.Where(x=>x.UserName == username)
            .Select(x=>x.ShoppingCart).SingleOrDefaultAsync();

            var relevantIds = cart.Select(x=>x.OrderedAnimalId);
          
            var animalBank = await _context.Animals
            .Where(x=>relevantIds.Contains(x.Id)).ToListAsync();

            IEnumerable<Animal> cartAnimals = new List<Animal>();

            foreach (var animal in cart)
            {
                cartAnimals.Append(animalBank.SingleOrDefault(x=>x.Id == animal.OrderedAnimalId));
            }

            return cartAnimals;
        }


    }
}