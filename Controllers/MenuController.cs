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
    [Authorize]
    public class MenuController : ControllerBase
    {
        MyDataContext _context;
        public MenuController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getmenu")]
        public async Task<ActionResult<List<menu>>> getmenu()
        {
            return await _context.Menu.ToListAsync();
        }

        [HttpDelete("deletebyid/{id}")]
        public async Task<ActionResult<menu>> delelebyid(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound("menu not found.");
            }
            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return Ok(menu);
        }

        [HttpPost("addmenu")]
        public async Task<ActionResult<menu>> addmenu(menu newMenu)
        {
            if(newMenu == null)
            {
                return BadRequest(newMenu);
            }
            _context.Menu.Add(newMenu);
            await _context.SaveChangesAsync();
            return Ok(newMenu);
        }

        [HttpPut("updateMenu/{id}")]
        public async Task<ActionResult<menu>> updateMenu(int id, menu newMenu)
        {
            var menu=await _context.Menu.FindAsync(id);
            if(menu == null)
            {
                return BadRequest(newMenu);
            }
            menu.title = newMenu.title;
            menu.price = newMenu.price;
            menu.image = newMenu.image;
            await _context.SaveChangesAsync();
            return Ok(menu);
        }
    }
}
