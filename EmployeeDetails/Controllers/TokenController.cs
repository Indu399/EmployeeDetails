using ApacheTech.Common.Extensions.System;
using Employee.Models;
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeDetails.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _tokenManager;

        public TokenController(IJwtTokenManager jwtTokenManager) 
        {
            _tokenManager = jwtTokenManager;
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost("Authenticate")]

        public IActionResult Authenticate([FromBody] EmployeeCredential credential)
        {
            var token = _tokenManager.Authenticate(credential.UserName, credential.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }

        //public async Task<IActionResult> Authenticate(EmployeeCredential credential)
        //{

        //    var token = new EmployeeCredential()
        //    {

        //        UserName = credential.UserName,
        //        Password = credential.Password


        //    };

        //    return Ok("You Logged Successfully");


        //}

    }
}
