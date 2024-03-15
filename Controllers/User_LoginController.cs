using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<List<user_login>> getUserLogin()
        {
            return _context.User_login.ToList();
        }

        [HttpPost]
        public ActionResult<user_login> addUserLogin(user_login newuser)
        {
            if(newuser != null)
            {
                _context.User_login.Add(newuser);
                _context.SaveChanges();
                return Ok(newuser);
            }
            return BadRequest(newuser);
        }
        [Authorize]
        [HttpPut("updateuserLoginpassword/{id}")]
        public ActionResult<user_login> updatepassword(int id,user_login data)
        {
            if(data != null)
            {
                var user = _context.User_login.Find(id);
                if(user == null)
                {
                    return BadRequest();
                }
                user.password= data.password;
                _context.SaveChanges();
                return Ok(user);
            }
            return BadRequest();
        }
    }
}
