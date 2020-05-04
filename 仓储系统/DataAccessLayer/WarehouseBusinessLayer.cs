﻿using System;
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
            List<Warehouse> newWarehouses = new List<Warehouse>();
            var models = dB.warehouses.Where(c => c.Wa_name == name);
            return models.ToList();
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
    }
}