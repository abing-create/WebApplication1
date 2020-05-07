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
        public StorageViewModel GetStorageViewModel(string U_name)
        {
            StorageViewModel storageViewModel = new StorageViewModel();
            storageViewModel.existTableListViewModel = new ExistTableListViewModel();
            storageViewModel.existTableListViewModel.existTableViewModels = new List<ExistTableViewModel>();
                        
            ExistBusinessLayer existBusinessLayer = new ExistBusinessLayer();
            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            OutIntoWareBusinessLayer outIntoWareBusinessLayer = new OutIntoWareBusinessLayer();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();

            //得到全部在存表中的数据
            List<Exist> exists = existBusinessLayer.GetExist();
            Out_Into_ware out_Into_Ware;
            ExistTableViewModel existTableViewModel;

            foreach (Exist item in exists)
            {
                existTableViewModel = new ExistTableViewModel();
                existTableViewModel.exist = item;
                out_Into_Ware = outIntoWareBusinessLayer.GetOut_Into_ware(item.IO_Id);
                //得到姓名
                existTableViewModel.U_name = userBusinessLayer.GetUsers("员工编号", out_Into_Ware.User_id.ToString()).FirstOrDefault().U_name;
                //得到仓库名
                existTableViewModel.W_name = warehouseBusinessLayer.GetWarehouse("仓库编号", out_Into_Ware.Ware_id.ToString()).FirstOrDefault().Wa_name;
                existTableViewModel.commodity = commodityBusinessLayer.GetCommodity("商品编号", item.Co_id.ToString()).FirstOrDefault();

                storageViewModel.existTableListViewModel.existTableViewModels.Add(existTableViewModel);
            }

            return storageViewModel;
        }
    }
}