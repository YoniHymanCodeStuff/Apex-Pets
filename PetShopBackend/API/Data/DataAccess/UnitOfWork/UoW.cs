using System.Threading.Tasks;
using API.Data.DataAccess.RepositoryClasses;
using API.Data.DataAccess.RepositoryInterfaces;
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
        }
        public IAnimalRepo animals => new AnimalRepo(_context,_mapper);
        public IAdminRepo admins => new AdminRepo(_context);
        public IPhotoRepo photos=> new PhotoRepo(_context);
        public ICustomerRepo customers => new CustomerRepo(_context);

        public IUserRepo users => new UserRepo(_context);

        public IOrderRepo orders => new OrderRepo(_context,_mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}