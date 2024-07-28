using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _RestaurantService;

        public RestaurantController(RestaurantService _Restaurant)
        {
            _RestaurantService = _Restaurant;
        }

        [HttpGet("GetRestaurants")]
        public List<Restaurant> GetRestaurants()
        {
            return _RestaurantService.GetRestaurants();
        }

        [HttpPost("AddRestaurant")]
        public Restaurant AddRestaurant(Restaurant Restaurant)
        {
            return _RestaurantService.AddRestaurant(new Restaurant
            {
                UserId = Restaurant.UserId,
                Address = Restaurant.Address,
                Name = Restaurant.Name,
                Image = Restaurant.Image,
                OpeningHours = Restaurant.OpeningHours,
                IsDelete = Restaurant.IsDelete,

            });
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_RestaurantService.GetIdRestaurant(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _RestaurantService.DeleteRestaurant(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Restaurant updatedRestaurant)
        {
            var updatedLoai = _RestaurantService.UpdateRestaurant(id, updatedRestaurant);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
