using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class payment
    {
        

        public int paymentId { get; set; }
        public string PaymentName { get; set; }

       
        public virtual ICollection<orders> orders { get; set; }
    }
}