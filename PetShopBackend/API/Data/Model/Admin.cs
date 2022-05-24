using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Model
{
    public class Admin
    {
        public int Id {get;set;}

        public string UserName { get; set; }
        
        public string FirstName {get;set;}
        public string LastName {get;set;}

        public string Email {get;set;}

        //public string ProfilePic { get; set; }

        public string City { get; set; }

        public byte[] hash { get; set; }

        public byte[] salt { get; set; }
        

               
        
        
        
    }
}