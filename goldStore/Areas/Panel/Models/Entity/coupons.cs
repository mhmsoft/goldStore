using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class coupons
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string couponCode { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> expired { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<bool> isUsed { get; set; }

        public virtual user user { get; set; }
    }
}