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
       
        public DeliveryAdress Address {get;set;}

         public ICollection<Order> Orders {get;set;} 

        public int PhoneNumber { get; set; } = 05000000;
        
                


    }
}