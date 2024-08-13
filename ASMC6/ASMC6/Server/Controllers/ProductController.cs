using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService _product)
        {
            _productService = _product;
        }

        [HttpGet("GetProducts")]
        public List<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        [HttpGet("GetProductsByRestaurant/{restaurantId}")]
        public ActionResult<List<Product>> GetProductsByRestaurant(int restaurantId)
        {
            var products = _productService.GetProductsByRestaurant(restaurantId);
            if (products == null || !products.Any())
            {
                return NotFound("No products found for this restaurant.");
            }

            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public Product AddProduct(Product product)
        {
            return _productService.AddProduct(new Product
            {
                MenuId = product.MenuId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                IsDelete = product.IsDelete,

            });
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<Product> GetId(int id)
        {
            if (id == 0)
            {
                return BadRequest("Value must be...");

            }
            return Ok(_productService.GetIdProduct(id));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _productService.DeleteProduct(id);
            if (deletedCategory == null)
            {
                return NotFound("Category not found");
            }

            return Ok(deletedCategory);
        }

        [HttpPut("PutProduct/{id}")]
        public IActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            var updatedLoai = _productService.UpdateProduct(id, updatedProduct);
            if (updatedLoai == null)
            {
                return NotFound("Category not found");
            }

            return Ok(updatedLoai);
        }


    }
}
