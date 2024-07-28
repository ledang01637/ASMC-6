using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class RestaurantService
    {
        private AppDBContext _context;
        public RestaurantService(AppDBContext context)
        {
            _context = context;
        }

        public List<Restaurant> GetRestaurants()
        {
            return _context.Restaurant.ToList();
        }
        public Restaurant AddRestaurant(Restaurant Restaurant)
        {
            _context.Add(Restaurant);
            _context.SaveChanges();
            return Restaurant;
        }
        public Restaurant DeleteRestaurant(int id)
        {
            var existingRes = _context.Restaurant.Find(id);
            if (existingRes == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingRes);
                _context.SaveChanges();
                return existingRes;
            }
        }
        public Restaurant GetIdRestaurant(int id)
        {
            var prod = _context.Restaurant.Find(id);
            if (prod == null)
            {
                return null;
            }
            return prod;
        }
        public Restaurant UpdateRestaurant(int id, Restaurant updateRestaurant)
        {
            var existingRes = _context.Restaurant.Find(id);
            if (existingRes == null)
            {
                return null;
            }
            existingRes.UserId = updateRestaurant.UserId;
            existingRes.Name = updateRestaurant.Name;
            existingRes.Image = updateRestaurant.Image;
            existingRes.Address = updateRestaurant.Address;
            existingRes.OpeningHours = updateRestaurant.OpeningHours;
            existingRes.IsDelete = updateRestaurant.IsDelete;


            _context.Update(existingRes);
            _context.SaveChanges();
            return existingRes;
        }
    }
}
