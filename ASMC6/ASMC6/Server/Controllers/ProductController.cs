using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASMC6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProducts")]
        public List<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        [HttpPost("AddProduct")]
        public ActionResult<Product> AddProduct(Product product)
        {
            var newProduct = _productService.AddProduct(new Product
            {
                MenuId = product.MenuId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                IsDelete = product.IsDelete,
            });

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, newProduct);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid product ID.");
            }

            var product = _productService.GetIdProduct(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deletedProduct = _productService.DeleteProduct(id);
            if (deletedProduct == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(deletedProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.ProductId)
            {
                return BadRequest("Product ID mismatch.");
            }

            var updatedProductResult = _productService.UpdateProduct(id, updatedProduct);
            if (updatedProductResult == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(updatedProductResult);
        }
    }
}