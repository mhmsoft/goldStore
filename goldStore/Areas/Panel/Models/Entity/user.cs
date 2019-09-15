using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public class user
    {
       
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rePassword { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string activationCode { get; set; }
        public string resetCode { get; set; }
        public string hostName { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> loginAttempt { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> loginTime { get; set; }
        public Nullable<bool> isMailVerified { get; set; }
        public Nullable<int> roleId { get; set; }
        public string city { get; set; }
        public Nullable<bool> subscribe { get; set; }
        public Nullable<int> postCode { get; set; }

        public virtual role role { get; set; }
        public virtual ICollection<wishlist> wishlist { get; set; }
        public virtual ICollection<coupons> coupons { get; set; }
        public virtual ICollection<orders> orders { get; set; }
    }
}