using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicineX.DBContext;
using MedicineX.DbModel;

namespace MedicineX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OrdersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return Ok(await _context.Orders.ToListAsync());
        }
        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult> GetUserOrder(int id)
        {
            var list = await _context.Orders.Where(o=>o.UserId==id).ToListAsync();
            if(list.Count() > 0)
            {
                return Ok(list);
            }
            return NotFound("No Order");
        }
        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderDto order)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'DatabaseContext.Orders'  is null.");
            }
            var neworder= new Order() 
            {
                UserId= order.UserId,
                MedicineId=order.MedicineId,
                Quantity=order.Quantity,
                Total=order.Total,
                OrderTime=DateTime.Now
            };
            var medi=await _context.Medicines.FindAsync(order.MedicineId);
            medi.Quantity=medi.Quantity-order.Quantity;

            _context.Orders.Add(neworder);
            await _context.SaveChangesAsync();

            return Ok("Order Placed");
        }

    }
}
