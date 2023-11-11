using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        MyDBContext _context;

        public ProductDAO(MyDBContext context)
        {
            _context = context;
        }

        public Product? GetById(int productId)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(m => m.ProductId == productId);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }

        public Boolean Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Delete(Product product)
        {
            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean Update(Product product)
        {
            try
            {
                Product? productUpdate = GetById(product.ProductId);
                if (productUpdate != null)
                {
                    productUpdate.ProductName = product.ProductName;
                    productUpdate.CategoryId = product.CategoryId;
                    productUpdate.Weight = product.Weight;
                    productUpdate.UnitPrice = product.UnitPrice;
                    productUpdate.UnitInStock = product.UnitInStock;
                    _context.Products.Update(productUpdate);
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
