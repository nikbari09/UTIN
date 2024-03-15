using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UTIN.DataContext;
using UTIN.Entities;

namespace UTIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        MyDataContext _context;
        IConfiguration _configuration;
        private SymmetricSecurityKey _key;
        public AuthController(MyDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult<user_login> login(user_login data)
        {
            var user = _context.User_login.FirstOrDefault(x => x.email == data.email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            if(user.password != data.password)
            {
                return BadRequest("Incorrect password.");
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, data.email),
                new Claim(JwtRegisteredClaimNames.UniqueName, data.email),
            };

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Issuer = _configuration["ValidIssuer"],
                Audience = _configuration["ValidAudience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscriptor);

            var AuthModel = new
            {
                token = tokenHandler.WriteToken(token),
                valid = token.ValidTo,
                role = user.role,
            };
            return Ok(AuthModel);
        }

    }
}
