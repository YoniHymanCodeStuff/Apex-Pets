using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DTOs;
using API.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class AdminRepo :  Repository<Admin>, IAdminRepo
    {
        private readonly DataContext _context;
        
        public AdminRepo(DataContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Admin> GetAdminAsync(string username)
        {
            return await _context.Admins
            .AsNoTracking()
            .Include(x=>x.Avatar)
            .FirstOrDefaultAsync(x=>x.UserName == username);
        }

        public async Task UpdateAdmin(BaseUserDto dto)
        {
            var admin = await _context.Admins.SingleOrDefaultAsync(x=>x.Id == dto.Id);

            admin.Avatar = dto.Avatar;
            admin.Email = dto.Email;
            admin.FirstName = dto.FirstName;
            admin.LastName = dto.LastName;
                       
            
        }
    }
}