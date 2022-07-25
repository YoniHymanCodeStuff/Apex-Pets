using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Data.Model
{
    public class Customer : User
    {

        public string CreditInfo { get; set; }
       
        public DeliveryAdress Address {get;set;} = new DeliveryAdress();

         public ICollection<Order> Orders {get;set;} 

         public ICollection<ShoppingCartItem> ShoppingCart {get;set;} 

        public int PhoneNumber { get; set; } = 05000000;
        
                


    }
}