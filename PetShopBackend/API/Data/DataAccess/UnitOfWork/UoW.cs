using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.RepositoryClasses;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DataAccess.generic_repository;


namespace API.Data.DataAccess.UnitOfWork
{
    public class UoW : IUoW
    {
       private readonly DataContext _context;

        public UoW(DataContext context)
        {
            _context = context;

        }
        public IAnimalRepo animals => new AnimalRepo(_context);
        public IAdminRepo admins => new AdminRepo(_context);
        public ICustomerRepo customers => new CustomerRepo(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // public void Dispose()
        // {
        //     _context.Dispose();
        // }
    }
}