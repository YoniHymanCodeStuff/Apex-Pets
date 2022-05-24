using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.Model;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class AdminRepo :  Repository<Admin>, IAdminRepo
    {
        private readonly DataContext _context;
        
        public AdminRepo(DataContext context) : base(context)
        {
            _context = context;

        }
    }
}