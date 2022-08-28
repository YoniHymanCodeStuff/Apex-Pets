using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DTOs;
using API.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Data.DataAccess.RepositoryInterfaces
{
    public interface IAdminRepo : IRepository<Admin>
    {
        Task<Admin> GetAdminAsync(string username);
       
        Task UpdateAdmin(BaseUserDto dto);
    }
}