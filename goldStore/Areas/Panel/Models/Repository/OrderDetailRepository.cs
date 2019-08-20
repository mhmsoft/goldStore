using goldStore.Areas.Panel.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Repository
{
    public class OrderDetailRepository : IRepository<orderDetails>
    {
        private goldstoreEntities _context;
        public OrderDetailRepository(goldstoreEntities Context)
        {
            _context = Context;
        }
        public List<product> BestSellProducts()
        {

            List<product> bestSellers = new List<product>();
            var query = _context.orderDetails.OrderByDescending(y => y.quantity).GroupBy(x => x.productId).
                        Select(x => new { quantity = x.Sum(b => b.quantity), Id = x.Key });

            var get10Products = query.Take(10);
            foreach (var item in get10Products)
            {
                bestSellers.Add(_context.product.SingleOrDefault(x => x.productId == item.Id));
            }
            return bestSellers;
        }
        public void Delete(orderDetails model)
        {
            if (model != null)
            {
                _context.orderDetails.Remove(model);
                _context.SaveChanges();
            }
        }

        public orderDetails Get(int id)
        {
            return _context.orderDetails.Find(id);
        }

        public List<orderDetails> GetAll()
        {
            return _context.orderDetails.AsNoTracking().ToList();
        }

        public void Save(orderDetails model)
        {
            if (model != null)
            {
                _context.orderDetails.Add(model);
                _context.SaveChanges();
            }
        }

        public void Update(orderDetails model)
        {
            orderDetails old = Get(model.orderId);
            _context.Entry(old).State = System.Data.Entity.EntityState.Detached;
            _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}