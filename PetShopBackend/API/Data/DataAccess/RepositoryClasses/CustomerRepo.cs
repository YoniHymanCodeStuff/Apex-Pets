using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.Model;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class CustomerRepo :  Repository<Customer>, ICustomerRepo
    {
        private readonly DataContext _context;
        
        public CustomerRepo(DataContext context) : base(context)
        {
            _context = context;

        }
    }
}