using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.RepositoryInterfaces;

namespace API.Data.DataAccess.UnitOfWork
{
    public interface IUoW 
    {
        IAnimalRepo animals {get;}
        IAdminRepo admins {get;}
        IPhotoRepo photos {get;}
        ICustomerRepo customers{get;}


        IUserRepo users{get;} 

        Task<bool> Complete();
    }
}