using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.Model;

namespace API.Data.DataAccess.RepositoryClasses
{
    //not really sure I should have made this, but it made my life easier
    public class PhotoRepo: Repository<Photo>, IPhotoRepo
    {
        private readonly DataContext _context;
        
        public PhotoRepo(DataContext context) : base(context)
        {
            _context = context;

        }
    }
}