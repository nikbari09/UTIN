using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTIN.DataContext;
using UTIN.Entities;

namespace UTIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        MyDataContext _context;
        public OffersController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getOffers")]
        public async Task<ActionResult<List<offers>>> getoffers()
        {
            return await _context.Offers.ToListAsync();
        }

        [HttpDelete("deletebyid/{id}")]
        public async Task<ActionResult<offers>> deleteoffer(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
                await _context.SaveChangesAsync();
                return Ok(offer);
            }
            return NotFound("Offer not found");
        }

        [HttpPost("addoffer")]
        public async Task<ActionResult<offers>> addnewOffer(offers offer)
        {
            if(offer == null)
            {
                return BadRequest();
            }
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
            return Ok(offer);
        }

        [HttpPut("updateOffer/{id}")]
        public async Task<ActionResult<offers>> updateoffer(int id, offers data)
        {
            if(data == null)
            {
                return BadRequest();
            }
            var offer = await _context.Offers.FindAsync(id);
            if(offer != null)
            {
                offer.title = data.title;
                offer.item1= data.item1;
                offer.item2= data.item2;
                offer.actual_price = data.actual_price;
                offer.discounted_price= data.discounted_price;
                await _context.SaveChangesAsync();
                return Ok(offer);
            }
            return BadRequest();
        }
    }
}
