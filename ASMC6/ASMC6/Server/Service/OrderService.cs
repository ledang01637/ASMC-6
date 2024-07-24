using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class OrderService
    {
        private AppDBContext _context;
        public OrderService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> GetOrder()
        {
            return _context.Order.ToList();
        }
        public Order AddOrder(Order Order)
        {
            _context.Add(Order);
            _context.SaveChanges();
            return Order;
        }
        public Order DeleteOrder(int id)
        {
            var existingOrder = _context.Order.Find(id);
            if (existingOrder == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingOrder);
                _context.SaveChanges();
                return existingOrder;
            }
        }
        public Order GetIdOrder(int id)
        {
            var prod = _context.Order.Find(id);
            if (prod == null)
            {
                return null;
            }
            return prod;
        }
        public Order UpdateOrder(int id, Order updateOrder)
        {
            var existingOrder = _context.Order.Find(id);
            if (existingOrder == null)
            {
                return null;
            }
            existingOrder.UserId = updateOrder.UserId;
            existingOrder.OrderDate = updateOrder.OrderDate;
            existingOrder.Status = updateOrder.Status;
            existingOrder.TotalAmount = updateOrder.TotalAmount;


            _context.Update(existingOrder);
            _context.SaveChanges();
            return existingOrder;
        }
    }
}
