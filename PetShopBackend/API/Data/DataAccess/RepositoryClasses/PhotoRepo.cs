using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.Model;

namespace API.Data.DataAccess.RepositoryClasses
{
   
    public class PhotoRepo: Repository<Photo>, IPhotoRepo
    {
        private readonly DataContext _context;
        
        public PhotoRepo(DataContext context) : base(context)
        {
            _context = context;

        }
    }
}