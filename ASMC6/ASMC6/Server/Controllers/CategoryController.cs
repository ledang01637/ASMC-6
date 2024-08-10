//using ASMC6.Server.Service;
//using ASMC6.Shared;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//namespace ASMC6.Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoryController : ControllerBase
//    {
//        private readonly ProductService __categoryService;

//        public CategoryController(CategoryService _category)
//        {
//            __categoryService = _category;
//        }

//        [HttpGet("GetCategory")]
//        public List<Category> GetCategories()
//        {
//            return __categoryService.GetCategory();
//        }

//        [HttpPost("AddProduct")]
//        public Product AddProduct(Product product)
//        {
//            return __categoryService.AddProduct(new Product
//            {
//                MenuId = product.MenuId,
//                CategoryId = product.CategoryId,
//                Name = product.Name,
//                Description = product.Description,
//                Image = product.Image,
//                Price = product.Price,
//                IsDelete = product.IsDelete,

//            });
//        }

//        [HttpGet("{id}")]
//        public ActionResult<Product> GetId(int id)
//        {
//            if (id == 0)
//            {
//                return BadRequest("Value must be...");

//            }
//            return Ok(__categoryService.GetIdProduct(id));
//        }


//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            var deletedCategory = __categoryService.DeleteProduct(id);
//            if (deletedCategory == null)
//            {
//                return NotFound("Category not found");
//            }

//            return Ok(deletedCategory);
//        }

//        [HttpPut("{id}")]
//        public IActionResult Update(int id, [FromBody] Category updatedCategory)
//        {
//            var updatedLoai = __categoryService.UpdateCategories(id, updatedCategory);s
//            if (updatedLoai == null)
//            {
//                return NotFound("Category not found");
//            }

//            return Ok(updatedLoai);
//        }
//    }
//}
