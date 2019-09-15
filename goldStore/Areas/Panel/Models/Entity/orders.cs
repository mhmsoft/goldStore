using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class orders
    {

        [Key]
        public int orderId { get; set; }
        public Nullable<int> customerId { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<bool> isOther { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public Nullable<int> postCode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Nullable<int> paymentType { get; set; }
        public Nullable<decimal> shipPrice { get; set; }
        public Nullable<decimal> orderPrice { get; set; }
        public Nullable<decimal> discountPrice { get; set; }

       
        public virtual ICollection<orderDetails> orderDetails { get; set; }
        public virtual user user { get; set; }
        public virtual payment payment { get; set; }
    }
}