using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.Model;

namespace API.Data.DataAccess.RepositoryInterfaces
{
    public interface IUserRepo : IRepository<User>
    {
        Task<bool> CheckIfIsAdminAsync(string username);  
    }
}