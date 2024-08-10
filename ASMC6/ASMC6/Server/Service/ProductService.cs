using ASMC6.Server.Data;
using ASMC6.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class ProductService
    {
        private AppDBContext _context;
        public ProductService(AppDBContext context)
        {
            _context = context;
        }
        public List<Product> GetProducts()
        {
            return _context.Product.ToList();
        }
        public Product AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }
        public Product DeleteProduct(int id)
        {
            var existingProd = _context.Product.Find(id);
            if (existingProd == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingProd);
                _context.SaveChanges();
                return existingProd;
            }
        }
        public Product GetIdProduct(int id)
        {
            var prod = _context.Product.Find(id);
            if (prod == null)
            {
                return null;
            }
            return prod;
        }
        public Product UpdateProduct(int id, Product updateProduct)
        {
            var existingprod = _context.Product.Find(id);
            if (existingprod == null)
            {
                return null;
            }
            existingprod.MenuId = updateProduct.MenuId;
            existingprod.CategoryId = updateProduct.CategoryId;
            existingprod.Name = updateProduct.Name;
            existingprod.Description = updateProduct.Description;
            existingprod.Image = updateProduct.Image;
            existingprod.Price = updateProduct.Price;
            existingprod.IsDelete = updateProduct.IsDelete;


            _context.Update(existingprod);
            _context.SaveChanges();
            return existingprod;
        }

    }
}
