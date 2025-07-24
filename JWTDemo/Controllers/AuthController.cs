using JWTDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace JWTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDAL _dal;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _dal = new UserDAL(configuration);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            var user = _dal.validateUser(login.Username, login.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = GenerateToken(user);
            return Ok(new { Token = token });

            //if (login.Username == "admin" && login.Password == "admin")
            //{
            //    var token = GenerateToken();
            //    return Ok(new { Token = token });
            //}
            //return Unauthorized();
        }

        private string GenerateToken(UserModel user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.DurationInMinutes));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
