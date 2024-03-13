using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTIN.DataContext;
using UTIN.Entities;

namespace UTIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        MyDataContext _context;
        public MenuController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getmenu")]
        public ActionResult<List<menu>> getmenu()
        {
            return _context.Menu.ToList();
        }

        [HttpDelete("deletebyid/{id}")]
        public ActionResult<menu> delelebyid(int id)
        {
            var menu = _context.Menu.Find(id);
            if (menu == null)
            {
                return NotFound("menu not found.");
            }
            _context.Menu.Remove(menu);
            _context.SaveChanges();
            return Ok(menu);
        }

        [HttpPost("addmenu")]
        public ActionResult<menu> addmenu(menu newMenu)
        {
            if(newMenu == null)
            {
                return BadRequest(newMenu);
            }
            _context.Menu.Add(newMenu);
            _context.SaveChanges();
            return Ok(newMenu);
        }

        [HttpPut("updateMenu/{id}")]
        public ActionResult<menu> updateMenu(int id, menu newMenu)
        {
            var menu=_context.Menu.Find(id);
            if(menu == null)
            {
                return BadRequest(newMenu);
            }
            menu.title = newMenu.title;
            menu.price = newMenu.price;
            menu.image = newMenu.image;
            _context.SaveChanges();
            return Ok(menu);
        }
    }
}
