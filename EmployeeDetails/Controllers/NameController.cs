using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        [HttpGet ("GetNames")]
        public  IActionResult GetNames() 
        {
            var token =  HttpContext.GetTokenAsync("access_token").Result;
            return Ok(new List<string> { "Adam","Robert"});
        }
    }
}
