using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IQueryable<Order> Query => _context.Orders;

        public Order Add(Order entity)
        {
            _context.Add(entity);
            return entity;
        }

        public Order Delete(Order entity)
        {
            _context.Remove(entity);
            return entity;
        }

        public Order Update(Order entity)
        {
            _context.Update(entity);
            return entity;
        }

        public List<Order> FetchAll()
        {
            return _context.Orders.ToList();
        }

        public Order Find(string id)
        {
            return _context.Find<Order>(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
