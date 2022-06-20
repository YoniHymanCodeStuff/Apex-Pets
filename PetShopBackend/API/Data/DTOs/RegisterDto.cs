using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.DTOs
{
    public class RegisterDto
    {

        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(12,MinimumLength = 3,ErrorMessage ="password must be between 3 and 12 characters long")]
        public string Password { get; set; }


                
        
    }
}