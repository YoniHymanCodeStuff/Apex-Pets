using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class Customer : User
    {

        public string CreditInfo { get; set; }
       
        public DeliveryAdress Adress {get;set;}

         public ICollection<Order> Orders {get;set;}

        
    }
}