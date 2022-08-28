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
using API.Services.authentication;
using API.Data.DTOs;
using API.helpers;
using AutoMapper;
using System.Security.Claims;
using API.Extensions;

namespace PetShop.PetShopBackend.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUoW _uow;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IUoW uow, ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            this._mapper = mapper;
            _uow = uow; 

        }
        [HttpPost("CreateAdmin")]
        public async Task<ActionResult<UserDto>> CreateAdmin(RegisterDto registerDto)
        {
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}
            
            
            if (await UserExists(registerDto.Username)) return BadRequest("username not available");

            using var hmac = new HMACSHA512();


            var photo = new Photo();
            _uow.photos.Add(photo);

            var admin = new Admin
            {
                UserName = registerDto.Username,
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                salt = hmac.Key,
                Avatar = photo,
                Email = registerDto.Email
            };
 
            _uow.admins.Add(admin);

            await _uow.Complete();

            return new UserDto { UserName = registerDto.Username, Token = _tokenService.CreateToken(admin), IsAdmin=true };
        }

        [HttpPost("Registration")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {


            if (await UserExists(registerDto.Username)) return BadRequest("username not available");

            using var hmac = new HMACSHA512();


            var photo = new Photo();
            _uow.photos.Add(photo);

            var customer = new Customer
            {
                UserName = registerDto.Username,
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)),
                salt = hmac.Key,
                Avatar = photo,
                Email = registerDto.Email
            };
 
            _uow.customers.Add(customer);

            await _uow.Complete();

            return new UserDto { UserName = registerDto.Username, Token = _tokenService.CreateToken(customer) };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await this._uow.users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            //checking for single user with this name in system: 
            if (user == null) return Unauthorized("Account does not exist");

            //validating pwd with hmac:
            using var hmac = new HMACSHA512(user.salt); //creating a hmac to use with the user input that already includes the salt 

            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDto.Password));//encrypting the user input with the salt from the username data. 

            //comparing the 2 hashcodes char by char (why can't we compare them as wholes?)
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.hash[i]) return Unauthorized("invalid password");
            }

            bool isAdmin = await _uow.users.CheckIfIsAdminAsync(loginDto.Username);
            

            return new UserDto { UserName = loginDto.Username,
             Token = _tokenService.CreateToken(user),
             IsAdmin = isAdmin };

        }

        [HttpPut("UpdateCustomer")]
       public async Task<ActionResult> UpdateCustomer(CustomerUpdateDto dto)
        {
           
            
            var username = User.GetUserName(); 
            var customer = (await _uow.customers.GetCustomerAsync(username)).Value;
           

            // map the DTO to the user automatically (otherwise we would have to do it manually)
            // no need for that: user.City = memberUpdateDTO.City;
            _mapper.Map(dto, customer); 

            _uow.customers.Update(customer);
             
            // now the entity is flagged as updated by EF (it's not saved yet and it doesn't matter if the entity was actually modified)

            if(await _uow.Complete())
            {
                return NoContent();
            }

            return BadRequest("Failed to update profile");

        }

        [HttpPatch("Admin")]
        public async Task<ActionResult> UpdateAdmin(BaseUserDto dto)
        {
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}
            
            await _uow.admins.UpdateAdmin(dto);

            if(await _uow.Complete())
            {
                return NoContent();
            }
            return BadRequest("Failed to update profile");
        }

        [HttpGet("Customer/{username}",Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(string username)
        {
            
            //need to et rid of the username input hee since we are just using the dat from the token
            
            var uusername =  User.GetUserName();
            var cust =  (await _uow.customers.GetCustomerAsync(uusername)).Value;

            var custDto = _mapper.Map<CustomerDto>(cust);

            return custDto;
        } 


        [HttpGet("Admin")]
        public async Task<ActionResult<BaseUserDto>> GetAdmin(){
            
            if(!User.GetIsAdmin()){return BadRequest("Only admins can execute this action.");}
                        
            var admin = await _uow.admins.GetAdminAsync(User.GetUserName());

            var dto = _mapper.Map<BaseUserDto>(admin);

            return dto;
        }



        
        public async Task<bool> GetIsUserAdmin(string username){
           
            return await _uow.users.CheckIfIsAdminAsync(username);
        }

        private async Task<bool> UserExists(string username)
        {
            //I should probably create a repo function for this. 
            var user = await _uow.users.SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());

            if (user == null) { return false; }
            return true;
        }

    }
}