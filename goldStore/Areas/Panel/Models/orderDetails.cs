//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace goldStore.Areas.Panel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class orderDetails
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual orders orders { get; set; }
        public virtual product product { get; set; }
    }
}
