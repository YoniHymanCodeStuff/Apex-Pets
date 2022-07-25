using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.RepositoryClasses;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DataAccess.generic_repository;
using AutoMapper;

namespace API.Data.DataAccess.UnitOfWork
{
    public class UoW : IUoW
    {
       private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UoW(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //I don't think I really should be injecting anything besides the 
            //datacontext in here... not great... 
        }
        public IAnimalRepo animals => new AnimalRepo(_context,_mapper);
        public IAdminRepo admins => new AdminRepo(_context);
        public IPhotoRepo photos=> new PhotoRepo(_context);//should this exist?
        public ICustomerRepo customers => new CustomerRepo(_context);

        public IUserRepo users => new UserRepo(_context);


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