using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class CommodityBusinessLayer
    {
        /// <summary>
        /// 查询数据库TblCommodity表
        /// </summary>
        /// <returns></returns>
        public List<Commodity> GetCommodity()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.commoditys.ToList();
        }

        public Commodity GetCommodity(string sid, Commodity commodity)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            Commodity model = null;
            switch (sid)
            {
                case "商品编号":
                    model = dB.commoditys.Where(c => c.Co_Id == commodity.Co_Id).FirstOrDefault();
                    break;
                case "商品名称":
                    model = dB.commoditys.Where(c => c.Co_name == commodity.Co_name).FirstOrDefault();
                    break;
                case "条码编号":
                    model = dB.commoditys.Where(c => c.Co_bar_code == commodity.Co_bar_code).FirstOrDefault();
                    break;
            }
            return model == null ? commodity : model;
        }

        /// <summary>
        /// 插入数据到表Tbl Commodity
        /// </summary>
        /// <param name="commodity">插入的数据</param>
        /// <returns></returns>
        public  Commodity InsertCommodity(Commodity commodity)
        {
            //dB.commoditys.Add(commodity);
            //dB.SaveChanges();
            //return commodity;

            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.commoditys.Add(commodity);
            dB.SaveChanges();
            return commodity;
        }

        private void Change(ref Commodity model, Commodity commodity)
        {
            model.Co_bar_code = commodity.Co_bar_code;
            model.Co_Id = commodity.Co_Id;
            model.Co_name = commodity.Co_name;
            model.Co_note = commodity.Co_note;
            model.Co_price = commodity.Co_price;
            model.Co_specification = commodity.Co_specification;
            model.Co_type = commodity.Co_type;
            model.Co_unit = commodity.Co_unit;
            model.Co_weight = commodity.Co_weight;
        }

        /// <summary>
        /// 修改数据到表TblCommodity
        /// </summary>
        /// <param name="name" Commodity的U_nam名字</param>
        /// <param name="commodity">更新成的表</param>
        public void UpdataCommoditys(string name,  Commodity commodity)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.commoditys.Where(c => c.Co_name == name).FirstOrDefault();
            if (model != null)
            {
                Change(ref model, commodity);
            }
        }

        /// <summary>
        /// 插入多个数据到表TblCommodity
        /// </summary>
        /// <param name="commoditys"></param>
        public void InsertCommoditys(List< Commodity> commoditys)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.commoditys.AddRange(commoditys);
            dB.SaveChanges();
        }
    }
}