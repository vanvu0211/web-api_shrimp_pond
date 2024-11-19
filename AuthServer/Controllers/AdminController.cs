using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AuthServer.Controllers
{



    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
   
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Admin role");
        }
    }
}
