using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(new[] { "test v1", "test v2", "test v3" });
        }

        [HttpGet("count")]
        public IActionResult GetCount()
        {
            return Ok(100);
        }
    }
}
