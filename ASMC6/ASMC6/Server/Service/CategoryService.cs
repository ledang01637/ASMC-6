using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class CategoryService
    {
        private AppDBContext _context;
        public CategoryService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategory()
        {
            return _context.Category.ToList();
        }
        public Category AddCategory(Category Category)
        {
            _context.Add(Category);
            _context.SaveChanges();
            return Category;
        }
        public Category DeleteCategory(int id)
        {
            var existingCate = _context.Category.Find(id);
            if (existingCate == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingCate);
                _context.SaveChanges();
                return existingCate;
            }
        }
        public Category GetIdCategory(int id)
        {
            var cate = _context.Category.Find(id);
            if (cate == null)
            {
                return null;
            }
            return cate;
        }
        public Category UpdateCategory(int id, Category updateCategory)
        {
            var existingCate = _context.Category.Find(id);
            if (existingCate == null)
            {
                return null;
            }
            existingCate.Name = updateCategory.Name;
            existingCate.Description = updateCategory.Description;

            _context.Update(existingCate);
            _context.SaveChanges();
            return existingCate;
        }
    }
}
