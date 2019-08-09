using goldStore.Areas.Panel.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Repository
{
    public class WishListRepository : IRepository<wishlist>
    {
        private goldstoreEntities _context;
        public WishListRepository(goldstoreEntities Context)
        {
            _context = Context;

        }
        public void Delete(wishlist model)
        {
            if (model != null)
            {
                _context.wishlist.Remove(model);
                _context.SaveChanges();
            }
        }

        public wishlist Get(int id)
        {
            return _context.wishlist.Find(id);
        }

        public List<wishlist> GetAll()
        {
            return _context.wishlist.AsNoTracking().ToList();
        }

        public void Save(wishlist model)
        {
            if (model != null)
            {
                _context.wishlist.Add(model);
                _context.SaveChanges();
            }
        }

        public void Update(wishlist model)
        {
            if (model != null)
            {
                wishlist old = Get(model.id);
                _context.Entry(old).State = System.Data.Entity.EntityState.Detached;
                _context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }
        }
    }
}