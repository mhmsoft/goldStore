using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Repository
{
    public class commentRepository
    {
        goldstoreEntities _context;
        public commentRepository(goldstoreEntities Context)
        {
            _context = Context;
        }

        public string yorumKaydet(comment yorum)
        {
            if (yorum != null)
            {
                _context.comment.Add(yorum);
                _context.SaveChanges();
                return "yorumunuz kaydedildi.";
            }
            else
                return "yorum kaydedilemedi.";
        }
    }
}