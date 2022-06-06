using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data.DTOs;
using API.Data.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.authentication
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            //token key is where we will save our inner key itself. its just an internal pointer to a string in appsettings.json. 
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(User user)
        {
            //who the user is claiming to be. and sets the claim name id as user.username: 
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName) 
            };

            //this defines the key and algorithm to create the token signature: 
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            //here we consolidate all the final data for the token: 
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds

            };

            //this object creates the token: 
            var tokenHandler = new JwtSecurityTokenHandler();

            //creating the token:
            var token = tokenHandler.CreateToken(tokenDescriptor);

            
            return tokenHandler.WriteToken(token);
        }

    }
}