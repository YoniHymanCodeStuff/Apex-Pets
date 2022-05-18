using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
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

 

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {

            return await _context.Animals.Select(x=>x.Category).Distinct().ToListAsync();

        }

        public async Task<IEnumerable<Animal>> GetCategoryAnimalsAsync(string category){

            return await _context.Animals.Where(x=>x.Category == category).ToListAsync();
            
        }


    }
}