using System;
using System.Text;
using Lif.Api.Models;
using Lif.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lif.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TestUserController : ControllerBase
    {
        readonly UserService userService;

        public TestUserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userService.GetUser());
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post()
        {
            return Ok(new
            {
                token = userService.GenerateToken(
                    DateTime.Now.AddDays(10),
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F42CEB76-1FD1-471B-8CBE-C732073023FE")), SecurityAlgorithms.HmacSha256Signature),
                    new User
                    {
                        Id = 11,
                        UserName = "test"
                    }
                )
            });
        }
    }
}