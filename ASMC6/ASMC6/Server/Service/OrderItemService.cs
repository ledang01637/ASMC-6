using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class OrderItemService
    {
        private AppDBContext _context;
        public OrderItemService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderItem> GetOrderItem()
        {
            return _context.OrderItem.ToList();
        }
        public OrderItem AddOrderItem(OrderItem OrderItem)
        {
            _context.Add(OrderItem);
            _context.SaveChanges();
            return OrderItem;
        }
        public OrderItem DeleteOrderItem(int id)
        {
            var existingOrderItem = _context.OrderItem.Find(id);
            if (existingOrderItem == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingOrderItem);
                _context.SaveChanges();
                return existingOrderItem;
            }
        }
        public OrderItem GetIdOrderItem(int id)
        {
            var oitem = _context.OrderItem.Find(id);
            if (oitem == null)
            {
                return null;
            }
            return oitem;
        }
        public OrderItem UpdateOrderItem(int id, OrderItem updateOrderItem)
        {
            var existingOrderItem = _context.OrderItem.Find(id);
            if (existingOrderItem == null)
            {
                return null;
            }
            existingOrderItem.ProductId = updateOrderItem.ProductId;
            existingOrderItem.OrderId = updateOrderItem.OrderId;
            existingOrderItem.Quantity = updateOrderItem.Quantity;
            existingOrderItem.Price = updateOrderItem.Price;


            _context.Update(existingOrderItem);
            _context.SaveChanges();
            return existingOrderItem;
        }
    }
}
