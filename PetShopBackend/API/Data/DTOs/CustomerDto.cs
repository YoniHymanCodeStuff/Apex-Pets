using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;

namespace API.Data.DTOs
{
    public class CustomerDto : BaseUserDto
    {

        public string CreditInfo { get; set; }
       
        public DeliveryAdress Address {get;set;}

         public ICollection<Order> Orders {get;set;}

         public int PhoneNumber { get; set; }
    }
}