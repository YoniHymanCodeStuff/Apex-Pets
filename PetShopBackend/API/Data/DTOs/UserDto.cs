
namespace API.Data.DTOs
{
    //I think this dto is just for registration purposes
    public class UserDto
    {
        public string UserName { get; set; }
        
        public string Token { get; set; }
        
        public bool IsAdmin {get;set;}
        
    }
}