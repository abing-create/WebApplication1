using demo1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace demo1.DataAccessLayer
{
    public class Class1ERPDAL : DbContext
    {
        public DbSet<Class1> Class1 { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class1>().ToTable("TblClass1");//TblEmployee代表表名
            base.OnModelCreating(modelBuilder);
        }
    }
}