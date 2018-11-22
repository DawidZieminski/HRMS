using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HRMS.Models;

namespace HRMS.DataBaseAccess
{
    public class HRMSContext : DbContext
    {
        public HRMSContext() : base("HRMSContext")
        {

        }

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Obiect> Obiect { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Presence> Presence { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }






    }
}