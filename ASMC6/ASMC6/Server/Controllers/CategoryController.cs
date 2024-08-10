using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService __categoryService;

        public CategoryController(CategoryService _category)
        {
            __categoryService = _category;
        }

        [HttpGet("GetCategories")]
        public List<Category> GetCategories()
        {
            return __categoryService.GetCategory();
        }

        [HttpPost("AddCategory")]
        public Category AddCategory(Category Category)
        {
            return __categoryService.AddCategory(new Category
            {
                Name = Category.Name,
                Description = Category.Description

            });
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(__categoryService.GetIdCategory(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = __categoryService.DeleteCategory(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category updatedCategory)
        {
            var updatedLoai = __categoryService.UpdateCategory(id, updatedCategory);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }
    }
}
