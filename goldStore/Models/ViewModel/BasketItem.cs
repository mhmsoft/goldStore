using goldStore.Areas.Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Models.ViewModel
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public product product { get; set; }
        public int quantity { get; set; }       
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }

    }
}