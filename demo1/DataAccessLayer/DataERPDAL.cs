using demo1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace demo1.DataAccessLayer
{
    public class DataERPDAL : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");//TblEmployee代表表名
            base.OnModelCreating(modelBuilder);
        }
    }
}