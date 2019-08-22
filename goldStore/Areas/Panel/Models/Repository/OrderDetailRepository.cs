using goldStore.Areas.Panel.Models.Interface;
using goldStore.Models.ViewModel;
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

        public List<GraphData> donutGraphValues()
        {

            List<GraphData> donutValues = new List<GraphData>();
            var query = _context.orderDetails.OrderByDescending(y => y.quantity).GroupBy(x => x.product.category.categoryName).
                        Select(x => new { total = x.Sum(b => b.quantity* b.product.price), category = x.Key });

            var getCategories = query.ToList();
            decimal sumTotal = (decimal)getCategories.Sum(x => x.total);
            foreach (var item in getCategories)
            {
                donutValues.Add(new GraphData {label=item.category,value=string.Format("{0:N2}",item.total/sumTotal*100) });
            }
            return donutValues;
        }
        public List<GraphData> donutGraphPaymentTypeValues()
        {

            List<GraphData> donutValues = new List<GraphData>();
            var query = _context.orderDetails.OrderByDescending(y => y.quantity).GroupBy(x => x.orders.Payment.PaymentName).
                        Select(x => new { ordertotal = x.Sum(b => b.orders.orderId), payment = x.Key });

            var getPayments = query.ToList();
            decimal sumTotal = (decimal)getPayments.Sum(x => x.ordertotal);
            foreach (var item in getPayments)
            {
                donutValues.Add(new GraphData { label = item.payment, value = string.Format("{0:N2}", item.ordertotal / sumTotal * 100) });
            }
            return donutValues;
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