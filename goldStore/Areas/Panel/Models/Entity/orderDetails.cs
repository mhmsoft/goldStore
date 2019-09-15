using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class orderDetails
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public Nullable<int> quantity { get; set; }
        public int Id { get; set; }

        public virtual product product { get; set; }
        public virtual orders orders { get; set; }
    }
}