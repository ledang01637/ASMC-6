using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _OrderService;

        public OrderController(OrderService _Order)
        {
            _OrderService = _Order;
        }

        [HttpGet("GetOrders")]
        public List<Order> GetOrders()
        {
            return _OrderService.GetOrder();
        }

        [HttpPost("AddOrder")]
        public Order AddOrder(Order Order)
        {
            return _OrderService.AddOrder(new Order
            {
                UserId = Order.UserId,
                OrderDate = Order.OrderDate,
                TotalAmount = Order.TotalAmount,
                Status = Order.Status

            });
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_OrderService.GetIdOrder(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _OrderService.DeleteOrder(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("UpdateStatus/{id}")]
        public IActionResult Update(int id, [FromBody] Order updatedOrder)
        {
            var updatedLoai = _OrderService.UpdateOrder(id, updatedOrder);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
