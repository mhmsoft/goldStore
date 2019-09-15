using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    
        public  class brand
        {
           
            public int brandId { get; set; }
            public string brandName { get; set; }
            public byte[] image { get; set; }

            
            public virtual ICollection<product> product { get; set; }
        }
    }

