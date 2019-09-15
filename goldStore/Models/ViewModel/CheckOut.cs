using goldStore.Areas.Panel.Models;
using goldStore.Areas.Panel.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Models.ViewModel
{
    public class CheckOut
    {
        public user user { get; set; }
        public List<BasketItem> Basket { get; set; }
    }
}