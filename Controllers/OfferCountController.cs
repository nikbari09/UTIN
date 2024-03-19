using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTIN.DataContext;
using UTIN.Entities;

namespace UTIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferCountController : ControllerBase
    {
        private readonly MyDataContext _context;
        public OfferCountController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getOffercount")]
        public async Task<ActionResult<List<offercount>>> getOffer()
        {
            return await _context.OfferCount.ToListAsync();
        }

        [HttpPost("addOffercount")]
        public ActionResult<offercount> setOfferCount(offercount count) 
        {
            if(count == null)
            {
                return BadRequest();
            }
            _context.OfferCount.Add(count);
            _context.SaveChanges();
            return Ok(count);    
        }

        [HttpDelete("deleteCount/{id}")]
        public ActionResult<offercount> deleteOfferCount(int id)
        {
            var data= _context.OfferCount.Find(id);
            if(data == null)
            {
                return BadRequest();
            }
            _context.OfferCount.Remove(data);
            _context.SaveChanges();
            return Ok(data);
        }


    }
}
