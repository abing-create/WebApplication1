using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class StorageBusinessLayer
    {
        /// <summary>
        /// 查询数据库TblStorage表
        /// </summary>
        /// <returns></returns>
        public List<Storage> GetStorage()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.storages.ToList();
        }

        /// <summary>
        /// 查询表的selectName成员为name的数据
        /// </summary>
        /// <param name="selectName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Storage> GetStorage(string selectName, string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Storage> storage;
            switch (selectName)
            {
                case "IO_Id":
                    storage = dB.storages.Where(c => c.IO_Id == name).ToList();
                    break;
                case "Co_id":
                    storage = dB.storages.Where(c => c.Co_id == Convert.ToInt32(name)).ToList();
                    break;
                case "IntoDate":
                    storage = dB.storages.Where(c => c.IntoDate.ToString() == name).ToList();
                    break;
                default:
                    storage = new List<Storage>();
                    break;
            }
            if (storage != null)
            {
                return storage;
            }
            return (new List<Storage>());
        }

        /// <summary>
        /// 插入数据到表TblStorage
        /// </summary>
        /// <param name="storage">插入的数据</param>
        /// <returns></returns>
        public Storage InsertStorage(Storage storage)
        {
            //dB.storages.Add(storage);
            //dB.SaveChanges();
            //return storage;

            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.storages.Add(storage);
            dB.SaveChanges();
            return storage;
        }

        /// <summary>
        /// 修改数据到表TblStorage
        /// </summary>
        /// <param name="name">Storage的U_nam名字</param>
        /// <param name="storage">更新成的表</param>
        public void UpdataStorages(string Co_id, string IO_id, Storage storage)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.storages.Where(c => c.Co_id.ToString() == Co_id && c.IO_Id == IO_id).FirstOrDefault();
            if (model != null)
            {
                //model.U_password = storage.U_password;
                Change(ref model, storage);
            }
            dB.SaveChanges();
        }

        private void Change(ref Storage model, Storage storage)
        {
            model.Count = storage.Count;
            model.Co_id = storage.Co_id;
            model.IO_Id = storage.IO_Id;
        }

        /// <summary>
        /// 插入多个数据到表TblStorage
        /// </summary>
        /// <param name="storages"></param>
        public void InsertStorages(List<Storage> storages)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.storages.AddRange(storages);
            dB.SaveChanges();
        }

        public void Delete(string IO_Id)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Storage> model = dB.storages.Where(c => c.IO_Id == IO_Id).ToList();
            dB.storages.RemoveRange(model);
            dB.SaveChanges();
        }

        public void Delete(string date, string Co_id, string IO_Id)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            DateTime date1 = Convert.ToDateTime(date).AddSeconds(-1);
            DateTime date2 = date1.AddSeconds(2);
            var model = dB.storages.Where(c => c.IO_Id == IO_Id && c.IntoDate.CompareTo(date1) != -1 && c.IntoDate.CompareTo(date2) != 1 && c.Co_id.ToString() == Co_id).ToList();
            dB.storages.Remove(model.FirstOrDefault());
            dB.SaveChanges();
        }
    }
}