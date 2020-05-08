using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.ViewModels;
using 仓储系统.Models;
using 仓储系统.DataAccessLayer;

namespace 仓储系统.BusinessLayer
{
    public class MyStorageBusinessLayer
    {
        /// <summary>
        /// 返回在存的全部数据
        /// </summary>
        /// <param name="U_name"></param>
        /// <returns></returns>
        public StorageViewModel GetStorageViewModel(bool Display, string U_name)
        {
            StorageViewModel storageViewModel = new StorageViewModel();
            storageViewModel.UserName = U_name;
            storageViewModel.existTableListViewModel = new ExistTableListViewModel();
            storageViewModel.existTableListViewModel.Display = Display ? "" : "none";
            storageViewModel.existTableListViewModel.existTableViewModels = new List<ExistTableViewModel>();
                        
            ExistBusinessLayer existBusinessLayer = new ExistBusinessLayer();
            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            OutIntoWareBusinessLayer outIntoWareBusinessLayer = new OutIntoWareBusinessLayer();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();

            //得到全部在存表中的数据
            List<Exist> exists = existBusinessLayer.GetExist();
            //Out_Into_ware out_Into_Ware;
            ExistTableViewModel existTableViewModel;

            foreach (Exist item in exists)
            {
                existTableViewModel = new ExistTableViewModel();
                existTableViewModel.exist = item;
                //out_Into_Ware = outIntoWareBusinessLayer.GetOut_Into_ware(item.IO_Id);
                //得到姓名
                existTableViewModel.U_name = userBusinessLayer.GetUsers("员工编号", item.U_id.ToString()).FirstOrDefault().U_name;
                //得到仓库名
                existTableViewModel.W_name = warehouseBusinessLayer.GetWarehouse("仓库编号", item.W_id.ToString()).FirstOrDefault().Wa_name;
                existTableViewModel.commodity = commodityBusinessLayer.GetCommodity("商品编号", item.Co_id.ToString()).FirstOrDefault();

                storageViewModel.existTableListViewModel.existTableViewModels.Add(existTableViewModel);
            }

            return storageViewModel;
        }

        /// <summary>
        /// 得到符合条件的在存数据
        /// </summary>
        /// <param name="U_name">登陆人</param>
        /// <param name="Select">属性</param>
        /// <param name="name">属性值</param>
        /// <returns></returns>
        public StorageViewModel GetStorageViewModel(bool Display, string U_name, string Select, string name)
        {
            StorageViewModel storageViewModel = new StorageViewModel();
            storageViewModel.UserName = U_name;
            storageViewModel.existTableListViewModel = new ExistTableListViewModel();
            storageViewModel.existTableListViewModel.Display = Display ? "" : "none";
            storageViewModel.existTableListViewModel.existTableViewModels = new List<ExistTableViewModel>();

            ExistBusinessLayer existBusinessLayer = new ExistBusinessLayer();
            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            OutIntoWareBusinessLayer outIntoWareBusinessLayer = new OutIntoWareBusinessLayer();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();

            //得到全部在存表中的数据
            List<Exist> exists;
            if(Select == "入库单号" || Select == "商品编号")
                exists = existBusinessLayer.GetExist(Select, name);
            else
                exists = existBusinessLayer.GetExist();
            //Out_Into_ware out_Into_Ware;
            ExistTableViewModel existTableViewModel;

            foreach (Exist item in exists)
            {
                existTableViewModel = new ExistTableViewModel();
                existTableViewModel.exist = item;
                //out_Into_Ware = outIntoWareBusinessLayer.GetOut_Into_ware(item.IO_Id);
                //得到姓名
                existTableViewModel.U_name = userBusinessLayer.GetUsers("员工编号", item.U_id.ToString()).FirstOrDefault().U_name;
                if (Select == "入库员工" && existTableViewModel.U_name != name)
                    continue;
                //得到仓库名
                existTableViewModel.W_name = warehouseBusinessLayer.GetWarehouse("仓库编号", item.W_id.ToString()).FirstOrDefault().Wa_name;
                if (Select == "存储仓库" && existTableViewModel.W_name != name)
                    continue;
                //existTableViewModel.commodity = commodityBusinessLayer.GetCommodity("商品编号", item.Co_id.ToString()).FirstOrDefault();
                CommodityMember commodityMember = new CommodityMember();
                switch(Select)
                {
                    case "商品编号":
                        commodityMember.Co_Id = name;
                        break;
                    case "商品名称":
                        commodityMember.Co_name = name;
                        break;
                    case "条码编号":
                        commodityMember.Co_bar_code = name;
                        break;
                    case "商品分类":
                        commodityMember.Co_type = name;
                        break;
                }
                existTableViewModel.commodity = commodityBusinessLayer.GetCommodities(commodityMember).FirstOrDefault();


                storageViewModel.existTableListViewModel.existTableViewModels.Add(existTableViewModel);
            }

            return storageViewModel;
        }
    }
}