using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess.generic_repository;
using API.Data.DataAccess.RepositoryInterfaces;
using API.Data.DTOs;
using API.Data.Model;
using API.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data.DataAccess.RepositoryClasses
{
    public class CustomerRepo :  Repository<Customer>, ICustomerRepo
    {
        private readonly DataContext _context;
        
        public CustomerRepo(DataContext context) : base(context)
        {
            _context = context;

        }

        

        public async Task<ActionResult<Customer>> GetCustomerAsync(string username)
        {
            return await _context.Customers
            .AsNoTracking()
            .Include(x=>x.ShoppingCart)
            .Include(a=>a.Address)
            .Include(x=>x.Avatar)
            .FirstOrDefaultAsync(x=>x.UserName == username);

         }

        public async Task<ActionResult<Customer>> GetCustomerForUpdates(string username)
        {
            return await _context.Customers
            .AsTracking()
            .Include(x=>x.Orders)
            .Include(x=>x.ShoppingCart)
            .Include(a=>a.Address)
            .FirstOrDefaultAsync(x=>x.UserName == username);

        }

        public async Task<IEnumerable<Order>> GetCustomerOrders(string customerName)
        {
            return await _context.Customers
            .AsNoTracking()
            .Where(x=>x.UserName == customerName)
            .Select(x=>x.Orders)
            .SingleOrDefaultAsync(); 

        }

    }
}