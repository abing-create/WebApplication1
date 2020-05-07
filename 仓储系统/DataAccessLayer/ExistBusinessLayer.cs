using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class ExistBusinessLayer
    {
        /// <summary>
        /// 查询仓库信息
        /// </summary>
        /// <returns></returns>
        public List<Exist> GetExist()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.exists.ToList();
        }

        private void Change(ref Exist model, Exist exists)
        {

        }

        /// <summary>
        /// 根据商品编号查询数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Exist> GetExist(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var models = dB.exists.Where(c => c.Co_id.ToString() == name);
            return models.ToList();
        }

        public List<Exist> GetExist(string select, string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Exist> models;
            switch (select)
            {
                case "入库单号":
                    models = dB.exists.Where(c => c.IO_Id == name).ToList();
                    break;
                case "商品编号":
                    models = dB.exists.Where(c => c.Co_id.ToString() == name).ToList();
                    break;
                default:
                    models = new List<Exist>();
                    break;
            }
            return models;
        }

        /// <summary>
        /// 修改Exist数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exists"></param>
        public void InputExist(string IO_Id, string Co_id, Exist exists)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.exists.Where(c => c.IO_Id == IO_Id && c.Co_id.ToString() == Co_id).FirstOrDefault();
            if (model != null)
            {
                //model.U_password = user.U_password;
                Change(ref model, exists);
            }
            dB.SaveChanges();
        }

        /// <summary>
        /// 存储仓库信息
        /// </summary>
        /// <param name="exists"></param>
        /// <returns></returns>
        public Exist InsertExist(Exist exists)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.exists.Add(exists);
            dB.SaveChanges();
            return exists;
        }

        public void DeleteExist(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.exists.Remove(dB.exists.Where(c => c.IO_Id == name).FirstOrDefault());
            dB.SaveChanges();
        }
    }
}