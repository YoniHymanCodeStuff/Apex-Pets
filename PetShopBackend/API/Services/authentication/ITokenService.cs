using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DTOs;
using API.Data.Model;

namespace API.Services.authentication
{
    public interface ITokenService
    {
        public string CreateToken(User user); 
    }
}