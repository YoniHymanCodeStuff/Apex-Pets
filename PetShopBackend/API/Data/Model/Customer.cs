using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class Customer
    {
        public int Id {get;set;}

        public string FirstName {get;set;}
        public string LastName {get;set;}

        public string Email {get;set;}

        //public string CreditInfo?? { get; set; }

        //public string avatar { get; set; } just a pic to put in corner

        public string UserName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public int houseNumber {get;set;}

        public string Zip {get;set;}

        
        public byte[] hash { get; set; }

        public byte[] salt { get; set; }
        
        
        
        
        

        
        
        
    }
}