using 仓库管理系统.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace 仓库管理系统.DataAccessLayer
{
    public class WarehouseERPDAL : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Record> records { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("TblUser");//TblUser代表表名
            modelBuilder.Entity<Record>().ToTable("TblRecord");
            base.OnModelCreating(modelBuilder);
        }
    }
}