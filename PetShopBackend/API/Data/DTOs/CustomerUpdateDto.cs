using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;

namespace API.Data.DTOs
{
    public class CustomerUpdateDto
    {
        public string CreditInfo { get; set; }
       
        public DeliveryAdress Address {get;set;}

        public int PhoneNumber { get; set; } 

        public string FirstName {get;set;}
        public string LastName {get;set;}
         public string Email {get;set;}

    }
}