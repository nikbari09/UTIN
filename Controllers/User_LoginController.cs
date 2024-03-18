using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTIN.DataContext;
using UTIN.Entities;

namespace UTIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_LoginController : ControllerBase
    {
        MyDataContext _context;
        public User_LoginController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getuser_login")]
        public async Task<ActionResult<List<user_login>>> getUserLogin()
        {
            return await _context.User_login.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<user_login>> addUserLogin(user_login newuser)
        {
            if(newuser != null)
            {
                _context.User_login.Add(newuser);
                await _context.SaveChangesAsync();
                return Ok(newuser);
            }
            return BadRequest(newuser);
        }
        [Authorize]
        [HttpPut("updateuserLoginpassword/{id}")]
        public async Task<ActionResult<user_login>> updatepassword(int id,user_login data)
        {
            if(data != null)
            {
                var user = await _context.User_login.FindAsync(id);
                if(user == null)
                {
                    return BadRequest();
                }
                user.password= data.password;
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            return BadRequest();
        }
    }
}
