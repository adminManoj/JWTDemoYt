using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult GetPublicData()
        {
            return Ok("This is public for all");
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult GetPrivateData()
        {
            return Ok("This is private");
        }
    }
}
