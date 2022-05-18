using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;

namespace API.Data.DataAccess.RepositoryInterfaces
{
    public interface IAnimalRepo : IRepository<Animal>
    {
        Task<IEnumerable<string>> GetCategoriesAsync(); 

        Task<IEnumerable<Animal>> GetCategoryAnimalsAsync(string category);

      
    }
}