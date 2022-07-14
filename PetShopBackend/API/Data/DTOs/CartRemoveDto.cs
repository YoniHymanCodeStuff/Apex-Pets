using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;

namespace API.Data.DTOs
{
    public class CartRemoveDto
    {
        public string username {get;set;}
        public ShoppingCartItem item {get;set;}
    }
}