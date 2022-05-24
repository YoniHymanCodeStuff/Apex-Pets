using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DTOs;

namespace API.Services.authentication
{
    public class TokenService : ITokenService
    {
        public Task<string> CreateToken(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}