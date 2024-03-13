using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<List<offers>> getoffers()
        {
            return _context.Offers.ToList();
        }

        [HttpDelete("deletebyid/{id}")]
        public ActionResult<offers> deleteoffer(int id)
        {
            var offer = _context.Offers.Find(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
                _context.SaveChanges();
                return Ok(offer);
            }
            return NotFound("Offer not found");
        }

        [HttpPost("addoffer")]
        public ActionResult<offers> addnewOffer(offers offer)
        {
            if(offer == null)
            {
                return BadRequest();
            }
            _context.Offers.Add(offer);
            _context.SaveChanges();
            return Ok(offer);
        }

        [HttpPut("updateOffer/{id}")]
        public ActionResult<offers> updateoffer(int id, offers data)
        {
            if(data == null)
            {
                return BadRequest();
            }
            var offer = _context.Offers.Find(id);
            if(offer != null)
            {
                offer.title = data.title;
                offer.item1= data.item1;
                offer.item2= data.item2;
                offer.actual_price = data.actual_price;
                offer.discounted_price= data.discounted_price;
                _context.SaveChanges();
                return Ok(offer);
            }
            return BadRequest();
        }
    }
}
