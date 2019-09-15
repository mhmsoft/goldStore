using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class role
    {

        public int roleId { get; set; }
        public string roleName { get; set; }

        public virtual ICollection<user> user { get; set; }
    }
}