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
        private readonly RestaurantService _resService;

        public RestaurantController(RestaurantService _restaurant)
        {
            _resService = _restaurant;
        }

        [HttpGet("GetRestaurant")]
        public List<Restaurant> GetRestaurant()
        {
            return _resService.GetRestaurant();
        }
    }
}
