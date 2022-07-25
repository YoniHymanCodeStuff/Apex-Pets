using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //4. we want to have hold of the user and username, 
            // * we don't believe to the client giving us the right username.
            // * we'll authenticate against the token, and we'll get the username from the token
            // * in the controller we have access to the ClaimsPrincipal (it's an object created from the token sent from the client side)
        public static string GetUserName(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;// I'm looking for the NameIdentifier claim (nameid in the payload in the jwt)
        }

    }
}