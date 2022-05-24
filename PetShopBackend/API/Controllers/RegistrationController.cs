using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.DataAccess;
using API.Data.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using API.utilities;
using API.Controllers;
using API.Data.Model;
using System.Security.Cryptography;

namespace PetShop.PetShopBackend.API.Controllers
{
    public class RegistrationController: BaseApiController{
        private readonly IUoW _uow;
        
        public RegistrationController(IUoW uow)
        {
            _uow = uow;
                                    
        }

        [HttpPost("Customer Registration")]
        public async Task<ActionResult<Customer>> Register(string username,string password)
        {
            using var hmac = new HMACSHA512();

            var customer = new Customer{
                UserName = username,
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)),
                salt = hmac.Key
            };

            _uow.customers.Add(customer);

            await _uow.Complete();

            return customer;
        } 

    }
}