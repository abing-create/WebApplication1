using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class WarehouseERPDAL : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Record> records { get; set; }
        public DbSet<Commodity> commoditys { get; set; }
        public DbSet<Out_Into_ware> out_into_wares { get; set; }
        //public DbSet<Out_ware> out_wares { get; set; }
        public DbSet<Storage> storages { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("TblUser");//TblUser代表表名
            modelBuilder.Entity<Record>().ToTable("TblRecord");
            modelBuilder.Entity<Record>().ToTable("TblCommodity");
            modelBuilder.Entity<Record>().ToTable("TblOut_Into_ware");
            //modelBuilder.Entity<Record>().ToTable("TblOut_ware");
            modelBuilder.Entity<Record>().ToTable("TblStorage");
            modelBuilder.Entity<Record>().ToTable("TblWarehouse");
            base.OnModelCreating(modelBuilder);
        }
    }
}