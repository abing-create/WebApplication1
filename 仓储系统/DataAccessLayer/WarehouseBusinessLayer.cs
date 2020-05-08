using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class WarehouseBusinessLayer
    {
        /// <summary>
        /// 查询仓库信息
        /// </summary>
        /// <returns></returns>
        public List<Warehouse> GetWarehouse()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.warehouses.ToList();
        }

        private void Change(ref Warehouse model, Warehouse warehouse)
        {
            model.Wa_able_capacity = warehouse.Wa_able_capacity;
            model.Wa_address = warehouse.Wa_address;
            model.Wa_capacity = warehouse.Wa_capacity;
            model.Wa_contact = warehouse.Wa_contact;
            model.Wa_Id = warehouse.Wa_Id;
            model.Wa_name = warehouse.Wa_name;
            model.Wa_note = warehouse.Wa_note;
            model.Wa_princiopal = warehouse.Wa_princiopal;
        }

        public List<Warehouse> GetWarehouse(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var models = dB.warehouses.Where(c => c.Wa_name == name);
            return models.ToList();
        }

        public List<Warehouse> GetWarehouse(string type, string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Warehouse> models;
            switch (type)
            {
                case "仓库名称":
                    models = dB.warehouses.Where(c => c.Wa_name == name).ToList();
                    break;
                case "仓库编号":
                    models = dB.warehouses.Where(c => c.Wa_Id.ToString() == name).ToList();
                    break;
                default:
                    models = new List<Warehouse>();
                    break;
            }
            return models.ToList();
        }

        public int GetId(string W_name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.warehouses.Where(c => c.Wa_name == W_name).FirstOrDefault();
            return model != null ? model.Wa_Id : -1;
        }

        /// <summary>
        /// 修改Warehouse数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="warehouse"></param>
        public void InputWarehouse(string name, Warehouse warehouse)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.warehouses.Where(c => c.Wa_name == name).FirstOrDefault();
            if (model != null)
            {
                //model.U_password = user.U_password;
                Change(ref model, warehouse);
            }
            dB.SaveChanges();
        }

        /// <summary>
        /// 存储仓库信息
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public Warehouse InsertWarehouse(Warehouse warehouse)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.warehouses.Add(warehouse);
            dB.SaveChanges();
            return warehouse;
        }

        public void DeleteWarehouse(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.warehouses.Remove(dB.warehouses.Where(c => c.Wa_name == name).FirstOrDefault());
            dB.SaveChanges();
        }
    }
}