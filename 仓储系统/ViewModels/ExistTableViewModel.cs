using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    /// <summary>
    /// 得到数据显示在ExitstTable中
    /// </summary>
    public class ExistTableViewModel
    {
        ///// <summary>
        ///// 出入库信息表，通过Exist表的入库单号查询到制表时间、仓库编号、出入库人
        ///// </summary>
        //public Out_Into_ware out_Into_Ware { get; set; }

        /// <summary>
        /// 入库人
        /// </summary>
        public string U_name { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string W_name { get; set; }

        /// <summary>
        /// 物品表，通过Exist表的物品编号得到商品名称、条码编号、商品分类、商品规格、商品单价、计量单位、商品重量
        /// </summary>
        public Commodity commodity { get; set; }

        /// <summary>
        /// Exist表的物品,记录物品的数量、入库日期
        /// </summary>
        public Exist exist { get; set; }
    }
}