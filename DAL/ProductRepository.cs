using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IQueryable<Product> Query => _context.Products;

        public Product Add(Product entity)
        {
            _context.Add(entity);
            return entity;
        }

        public Product Delete(Product entity)
        {
            _context.Remove(entity);
            return entity;
        }

        public List<Product> FetchAll()
        {
            return _context.Products.ToList();
        }

        public Product Update(Product entity)
        {
            _context.Update(entity);
            return entity;
        }

        public Product Find(string id)
        {
            return _context.Find<Product>(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
