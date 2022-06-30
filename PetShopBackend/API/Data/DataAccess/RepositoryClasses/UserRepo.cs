using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        private readonly DataContext _context;
        
        public UserRepo(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<bool> CheckIfIsAdminAsync(string username)
        {
            
            return  await  _context.Users.Where(x=>x.UserName == username)
            .Select(x=>x.UserType).SingleOrDefaultAsync() == "admin";

            // var user = await  _context.Users.SingleOrDefaultAsync(x=> x.UserName == username);

            // var type = user.UserType;

            // return type == "admin";
        }
    }
}