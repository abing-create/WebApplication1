using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.GenerateBarcode;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    //public enum CommodityMember
    //{
    //    Co_Id, Co_name, Co_bar_code, Co_type, Co_specification, Co_price, Co_unit, Co_weight
    //}

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

        public List<Commodity> GetCommodities(CommodityMember commodityMember)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Commodity> commodities;
            commodities = dB.commoditys.Where(c => (commodityMember.Co_bar_code == null || c.Co_bar_code == commodityMember.Co_bar_code) &&
            (commodityMember.Co_Id == null || c.Co_Id.ToString() == commodityMember.Co_Id) &&
            (commodityMember.Co_name == null || c.Co_name == commodityMember.Co_name) &&
            (commodityMember.Co_note == null || c.Co_note == commodityMember.Co_note) &&
            (commodityMember.Co_price == null || c.Co_price.ToString() == commodityMember.Co_price) &&
            (commodityMember.Co_specification == null || c.Co_specification == commodityMember.Co_specification) &&
            (commodityMember.Co_type == null || c.Co_type == commodityMember.Co_type) &&
            (commodityMember.Co_unit == null || c.Co_unit == commodityMember.Co_unit) &&
            (commodityMember.Co_weight == null || c.Co_weight.ToString() == commodityMember.Co_weight)).ToList();

            return commodities == null ? new List<Commodity>() : commodities;
        }

        /// <summary>
        /// 返回Select属性为name的物品的集合
        /// </summary>
        /// <param name="Select"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Commodity> GetCommodity(string Select, string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Commodity> commodities;
            switch(Select)
            {
                case "商品编号":
                    commodities = dB.commoditys.Where(c => c.Co_Id.ToString() == name).ToList();
                    break;
                case "商品名称":
                    commodities = dB.commoditys.Where(c => c.Co_name == name).ToList();
                    break;
                case "条码编号":
                    commodities = dB.commoditys.Where(c => c.Co_bar_code == name).ToList();
                    break;
                case "商品分类":
                    commodities = dB.commoditys.Where(c => c.Co_type == name).ToList();
                    break;
                case "商品规格":
                    commodities = dB.commoditys.Where(c => c.Co_specification == name).ToList();
                    break;
                case "商品单价":
                    commodities = dB.commoditys.Where(c => c.Co_price.ToString() ==name).ToList();
                    break;
                case "商品重量":
                    commodities = dB.commoditys.Where(c => c.Co_weight.ToString() == name).ToList();
                    break;
                default:
                    commodities = new List<Commodity>();
                    break;
            }
            return commodities;
        }

        /// <summary>
        /// 返回表中第一个符合条件的值
        /// </summary>
        /// <param name="Co_Id">商品号</param>
        /// <param name="Co_name">商品名称</param>
        /// <param name="Co_bar_code">条码编号</param>
        /// <returns></returns>
        public Commodity GetCommodity(int Co_Id, string Co_name, string Co_bar_code)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Commodity> commodities = dB.commoditys.ToList();
            if(Co_Id != 0)
            {
                commodities = commodities.Where(c => c.Co_Id == Co_Id).ToList();
            }
            if(Co_name != "")
            {
                commodities = commodities.Where(c => c.Co_name == Co_name).ToList();
            }
            if(Co_bar_code != "")
            {
                commodities = commodities.Where(c => c.Co_bar_code == Co_bar_code).ToList();
            }
            return commodities.FirstOrDefault();
        }

        /// <summary>
        /// 查找和commodity的sid属性相同的第一个Commodity
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="commodity"></param>
        /// <returns></returns>
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
        public  bool InsertCommodity(Commodity commodity, string path)
        {
            //dB.commoditys.Add(commodity);
            //dB.SaveChanges();
            //return commodity;
            using (WarehouseERPDAL dB = new WarehouseERPDAL())
            {
                try
                {
                    dB.commoditys.Add(commodity);
                    dB.SaveChanges();
                    BarCode barcode = new BarCode();
                    barcode.Code(commodity.Co_bar_code, path);

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
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
        public bool UpdataCommoditys(string id,  Commodity commodity, string path)
        {
            using (WarehouseERPDAL dB = new WarehouseERPDAL())
            {
                try
                {
                    var model = dB.commoditys.Where(c => c.Co_Id.ToString() == id).FirstOrDefault();
                    if (model != null && !model.Equals(new Commodity()))
                    {
                        BarCode barCode = new BarCode();
                        if (model.Co_bar_code != commodity.Co_bar_code)
                        {
                            barCode.DeleteFile(path + model.Co_bar_code + ".jdg");
                            barCode.Code(commodity.Co_bar_code, path);
                        }
                        Change(ref model, commodity);
                    }
                    dB.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
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

        /// <summary>
        /// 移除名称为编号为id的物品
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCommodity(string id, string path)
        {
            //int my_id = Convert.ToInt32(id);
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.commoditys.Where(c => c.Co_Id.ToString() == id).FirstOrDefault();
            if (model != null && !model.Equals(new Commodity()))
            {
                BarCode barCode = new BarCode();
                barCode.DeleteFile(path);
                dB.commoditys.Remove(model);
            }
            dB.SaveChanges();
        }
    }
}