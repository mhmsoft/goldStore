using goldStore.Areas.Panel.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Repository
{
    public class CouponRepository:IRepository<coupons>
    {
        private goldstoreEntities _context;
        public CouponRepository(goldstoreEntities Context)
        {
            _context = Context;

        }
        public void Delete(coupons model)
        {
            if (model!=null)
            {
                _context.coupons.Remove(model);
                _context.SaveChanges();
            }
        }
        public coupons Get(int id)
        {
            return _context.coupons.Find(id);
        }

        public List<coupons> GetAll()
        {
            return _context.coupons.AsNoTracking().ToList();
        }
        public void Save(coupons model)
        {
            if (model != null)
            {
                _context.coupons.Add(model);
                _context.SaveChanges();
            }
        }
        public void Update(coupons model)
        {   if(model!=null)
            {
                coupons old = Get(model.Id);
                _context.Entry(old).State = System.Data.Entity.EntityState.Detached;
                _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}