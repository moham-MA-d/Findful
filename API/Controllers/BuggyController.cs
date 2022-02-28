using API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data;

namespace API.Controllers
{
    //it's purpose is to return error and we can see what we get back from varrious responses  
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _dataContext;
        public BuggyController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [Authorize]
        [HttpGet("auth")]
        public  ActionResult<string> GetSecret() 
        {
            return "secret";
        }


        
        [HttpGet("not-found")]
        public  ActionResult<string> GeNoutFound() 
        {
            var thing = _dataContext.Users.Find(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }


        
        [HttpGet("server-error")]
        public  ActionResult<string> GetServerError() 
        {
            var thing = _dataContext.Users.Find(-1);
            var returnThing = thing.ToString();

            return returnThing;
        }


        
        [HttpGet("bad-request")]
        public  ActionResult<string> GetbadRequest() 
        {
            return BadRequest("it is a bad request!") ;
        }

        
    }
}