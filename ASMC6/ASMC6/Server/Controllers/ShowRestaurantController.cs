using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowRestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;
        public ShowRestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_restaurantService.GetRestaurantByIdUser(id));
        }
    }
}
