using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace goldStore.Areas.Panel.Models.Entity
{
    public  class goldstoreEntities : DbContext
    {
        public goldstoreEntities()
            : base("name=goldstore")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
        }

        public virtual DbSet<brand> brand { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<productImage> productImage { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<wishlist> wishlist { get; set; }
        public virtual DbSet<payment> payment { get; set; }
        public virtual DbSet<orderDetails> orderDetails { get; set; }
        public virtual DbSet<coupons> coupons { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<comment> comment { get; set; }
    }
}