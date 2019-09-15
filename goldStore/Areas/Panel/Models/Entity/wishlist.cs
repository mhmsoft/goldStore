using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class wishlist
    {
        public int id { get; set; }
        public Nullable<int> productId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<System.DateTime> created { get; set; }

        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}