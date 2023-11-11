using BusinessObject.Models;

namespace DataAccess
{
    public class CategoryDAO
    {
        MyDBContext _context;

        public CategoryDAO(MyDBContext context)
        {
            _context = context;
        }

        public Category? GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Boolean Create(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Delete(Category category)
        {
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Update(Category category)
        {
            try
            {
                Category? categoryUpdate = GetById(category.CategoryId);
                if (categoryUpdate != null)
                {
                    categoryUpdate.CategoryName = category.CategoryName;
                    _context.Categories.Update(categoryUpdate);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
