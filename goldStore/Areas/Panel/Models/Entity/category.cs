using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class category
    {
        
           

            public int categoryId { get; set; }
            public string categoryName { get; set; }
            public byte[] image { get; set; }

           
            public virtual ICollection<product> product { get; set; }
        
    }
}