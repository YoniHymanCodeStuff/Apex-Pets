using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
      
        public static string GetUserName(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static bool GetIsAdmin(this ClaimsPrincipal user){
            var claim = user.FindFirst(x=>x.Type == "userType")?.Value;

            if (claim == "Admin") return true;
            return false; 
        }

    }
}