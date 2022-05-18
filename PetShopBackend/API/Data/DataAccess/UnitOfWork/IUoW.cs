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

        Task<bool> Complete();
    }
}