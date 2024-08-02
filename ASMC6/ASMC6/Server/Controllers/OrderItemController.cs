using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _OrderItemService;

        public OrderItemController(OrderItemService _OrderItem)
        {
            _OrderItemService = _OrderItem;
        }

        [HttpGet("GetOrderItems")]
        public List<OrderItem> GetOrderItems()
        {
            return _OrderItemService.GetOrderItem();
        }

        [HttpPost("AddOrderItem")]
        public OrderItem AddOrderItem(OrderItem OrderItem)
        {
            return _OrderItemService.AddOrderItem(new OrderItem
            {
                OrderId = OrderItem.OrderId,
                ProductId = OrderItem.ProductId,
                Price = OrderItem.Price,
                Quantity = OrderItem.Quantity

            });
        }

        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_OrderItemService.GetIdOrderItem(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _OrderItemService.DeleteOrderItem(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OrderItem updatedOrderItem)
        {
            var updatedLoai = _OrderItemService.UpdateOrderItem(id, updatedOrderItem);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
