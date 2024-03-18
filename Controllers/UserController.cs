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
    public class UserController : ControllerBase
    {
        MyDataContext _context;
        public UserController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getUsers")]
        public async Task<ActionResult<List<users>>> getUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("addUsers")]
        public async Task<ActionResult<users>> addUsers(users newuser)
        {
           var validate=await _context.Users.FirstOrDefaultAsync(x=>x.email == newuser.email);
           if(validate == null)
            {
                if (newuser != null)
                {
                    newuser.role = "user";
                    _context.Users.Add(newuser);
                    await _context.SaveChangesAsync();
                    return Ok(newuser);
                }
                return NotFound("invalid");
            }
            return BadRequest("user exits");
          
        }
        [Authorize]
        [HttpPut("updatepassword/{id}")]
        public async Task<ActionResult<users>> updatepassword(int id, users data)
        {
            if(data != null)
            {
                var user = await _context.Users.FindAsync(id);
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
