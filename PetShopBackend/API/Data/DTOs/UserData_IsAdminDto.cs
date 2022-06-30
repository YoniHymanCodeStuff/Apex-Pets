using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Data.DTOs
{
    public class UserData_IsAdminDto
    {
        public bool IsAdmin { get; set; }
        
        public ActionResult<Customer> customer {get;set;}
        public ActionResult<Admin> admin {get;set;}
    }
}