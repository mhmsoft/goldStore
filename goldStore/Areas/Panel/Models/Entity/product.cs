using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class product
    {
       

        public int productId { get; set; }
        public string productName { get; set; }
        public Nullable<decimal> oldPrice { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> stock { get; set; }
        public string description { get; set; }
        public Nullable<int> categoryId { get; set; }
        public Nullable<int> brandId { get; set; }
        public Nullable<System.DateTime> created { get; set; }

        public virtual brand brand { get; set; }
        public virtual category category { get; set; }
        
        public virtual ICollection<productImage> productImage { get; set; }

        public virtual ICollection<wishlist> wishlist { get; set; }
        public virtual ICollection<orderDetails> orderDetails { get; set; }
        public virtual ICollection<comment> comment { get; set; }
    }
}