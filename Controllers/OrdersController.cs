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
    public class OrdersController : ControllerBase
    {
        MyDataContext _context;
        private object x;

        public OrdersController(MyDataContext context)
        {
            _context = context;
        }

        [HttpGet("getOrders")]
        public async Task<ActionResult<List<orders>>> getOrders()
        {
            return await _context.Orders.Include(o=>o.details).ThenInclude(d=>d.address).ToListAsync();
        }

        [HttpPut("updateStatus/{id}")]
        public async Task<ActionResult<orders>> updateStatus(int id, orders neworder)
        {
            try
            {
                var order = await _context.Orders.Include(o => o.details).FirstOrDefaultAsync(o => o.id == id);

                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                // Assuming there is only one detail for each order
                var orderDetail = order.details.FirstOrDefault();

                if (orderDetail != null)
                {
                    orderDetail.status = neworder.details[0].status;

                    if (neworder.details[0].status == "Delivered")
                    {
                        order.time = neworder.time;
                    }
                    await _context.SaveChangesAsync();
                    return Ok(order);
                }
                return BadRequest("Order detail not found.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("addOrder")]
        public async Task<ActionResult<orders>> addOrder(orders order)
        {
            if(order == null)
            {
                return NotFound("data not found.");
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
    }
}
