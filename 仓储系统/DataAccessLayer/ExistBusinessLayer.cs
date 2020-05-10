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
            model.Count = exists.Count;
            model.Co_id = exists.Co_id;
            model.IntoDate = exists.IntoDate;
            model.IO_Id = exists.IO_Id;
            model.U_id = exists.U_id;
            model.W_id = exists.W_id;
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
        /// 修改Exist数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exists"></param>
        public void InputExist(string IO_Id, Exist exists)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.exists.Where(c => c.IO_Id == IO_Id).FirstOrDefault();
            if (model != null)
            {
                //model.U_password = user.U_password;
                Change(ref model, exists);
            }
            dB.SaveChanges();
        }

        /// <summary>
        /// 修改Exist数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exists"></param>
        public void InputExist(string IO_Id, string Co_id, int Count)
        {
            using (WarehouseERPDAL dB = new WarehouseERPDAL())
            {
                try
                {
                    var model = dB.exists.Where(c => c.IO_Id == IO_Id && c.Co_id.ToString() == Co_id).FirstOrDefault();
                    if (model != null)
                    {
                        if (Count == model.Count)
                        {
                            dB.exists.Remove(model);
                        }
                        else if (model.Count > Count)
                        {
                            model.Count -= Count;
                        }
                    }
                    dB.SaveChanges();
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 存储仓库信息
        /// </summary>
        /// <param name="exists"></param>
        /// <returns></returns>
        public Exist InsertExist(Exist exists)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Exist> model = dB.exists.Where(c => c.IO_Id == exists.IO_Id && exists.W_id == c.W_id).ToList();
            if(model != null && model.Count() > 0)
            {
                model.FirstOrDefault().Count += exists.Count;
            }
            else
            {
                dB.exists.Add(exists);
            }
            dB.SaveChanges();
            return exists;
        }

        public void Delete(Storage storage)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.exists.Where(c => c.Co_id == storage.Co_id).ToList();
            int count = storage.Count;
            if(model != null)
            {
                while(count > 0 && model.Count() != 0)
                {
                    if(count >= model.FirstOrDefault().Count)
                    {
                        count -= model.FirstOrDefault().Count;
                        dB.exists.Remove(model.FirstOrDefault());
                        model.Remove(model.FirstOrDefault());
                    }
                    else
                    {
                        model.FirstOrDefault().Count -= count;
                        count = 0;
                    }
                }
            }
            dB.SaveChanges();
        }

        public void DeleteExist(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.exists.Remove(dB.exists.Where(c => c.IO_Id == name).FirstOrDefault());
            dB.SaveChanges();
        }
    }
}