using System.Data.Entity;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class WarehouseERPDAL : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Record> records { get; set; }
        public DbSet<Commodity> commoditys { get; set; }
        public DbSet<Out_Into_ware> out_into_wares { get; set; }
        public DbSet<Storage> storages { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }
        public DbSet<Exist> exists { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("TblUser");//TblUser代表表名
            modelBuilder.Entity<Record>().ToTable("TblRecord");
            modelBuilder.Entity<Commodity>().ToTable("TblCommodity");
            modelBuilder.Entity<Out_Into_ware>().ToTable("TblOut_Into_ware");
            modelBuilder.Entity<Storage>().ToTable("TblStorage");
            modelBuilder.Entity<Warehouse>().ToTable("TblWarehouse");
            modelBuilder.Entity<Exist>().ToTable("TblExist");
            base.OnModelCreating(modelBuilder);
        }
    }
}