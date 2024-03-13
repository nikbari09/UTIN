using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<List<users>> getUsers()
        {
            return _context.Users.ToList();
        }

        [HttpPost("addUsers")]
        public ActionResult<users> addUsers(users newuser)
        {
           var validate=_context.Users.FirstOrDefault(x=>x.email == newuser.email);
           if(validate == null)
            {
                if (newuser != null)
                {
                    newuser.role = "user";
                    _context.Users.Add(newuser);
                    _context.SaveChanges();
                    return Ok(newuser);
                }
                return NotFound("invalid");
            }
            return BadRequest("user exits");
          
        }

        [HttpPut("updatepassword/{id}")]
        public ActionResult<users> updatepassword(int id, users data)
        {
            if(data != null)
            {
                var user = _context.Users.Find(id);
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
