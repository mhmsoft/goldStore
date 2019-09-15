using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class comment
    {
        public int commentId { get; set; }
        public Nullable<int> productId { get; set; }
        public string review { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string email { get; set; }

        public virtual product product { get; set; }
    }
}