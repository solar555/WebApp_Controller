using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp_Controller.Models;

namespace WebApp_Controller.DataAccessLayer
{
    public class SalesERPDAL : DbContext
    {
        public SalesERPDAL() : base("SalesERPDAL")
        {

        }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }
    }
}