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

        IOrderRepo orders{get;}


        IUserRepo users{get;} 

        Task<bool> Complete();
    }
}