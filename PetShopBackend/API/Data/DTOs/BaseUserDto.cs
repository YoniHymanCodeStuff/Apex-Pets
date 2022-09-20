using API.Data.Model;

namespace API.Data.DTOs
{
    public class BaseUserDto
    {
        
         public int Id {get;set;}

        public string FirstName {get;set;}
        public string LastName {get;set;}

        public string UserName { get; set; }

       
        public string Email {get;set;}

        public Photo Avatar { get; set;}
    }
}