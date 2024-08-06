using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _MenuService;

        public MenuController(MenuService _Menu)
        {
            _MenuService = _Menu;
        }

        [HttpGet("GetMenus")]
        public List<Menu> GetMenus()
        {
            return _MenuService.GetMenus();
        }

        [HttpPost("AddMenu")]
        public Menu AddMenu(Menu Menu)
        {
            return _MenuService.AddMenu(new Menu
            {
                RestaurantId = Menu.RestaurantId,
                Name = Menu.Name,
                IsDelete = Menu.IsDelete,

            });
        }

        [HttpGet("{id}")]
        public ActionResult<Menu> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_MenuService.GetIdMenu(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _MenuService.DeleteMenu(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Menu updatedMenu)
        {
            var updatedLoai = _MenuService.UpdateMenu(id, updatedMenu);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
