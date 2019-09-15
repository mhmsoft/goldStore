using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class productImage
    {
        [Key]
        public int imageId { get; set; }
        public byte[] image { get; set; }
        public Nullable<int> productId { get; set; }

        public virtual product product { get; set; }
    }
}