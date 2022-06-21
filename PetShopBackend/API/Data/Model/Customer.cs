using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class Customer : User
    {


        //I made these separate fields bc for employee email is required. 
        public string Email {get;set;}

        //public string CreditInfo?? { get; set; }

        
        //might make sense to just have a linked "adress" entity. 
        public string City { get; set; }
        public string Street { get; set; }

        public int houseNumber {get;set;}

        public string Zip {get;set;}

         public Photo Avatar { get; set;}

        

    }
}