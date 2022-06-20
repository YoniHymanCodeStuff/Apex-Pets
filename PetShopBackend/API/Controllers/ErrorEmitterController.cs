using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess;
using API.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    
    public class ErrorEmitterController : BaseApiController
    {
        //since this is just for testing this, I wont bother with
        //setting up the whole Repository-uow business for this: 
        private readonly DataContext _context;
        public ErrorEmitterController(DataContext context)
        {
            _context = context;
            
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret string";
            
        }

        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound(){
            var thing = _context.Users.Find(-1);
            if (thing==null)
            {
                return NotFound();
            }
            return Ok(thing);
        }

        
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);
            var thingtostring = thing.ToString();
            return thingtostring;
            
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("that was a pathetic and misguided request.");
            
        }
    }
}