using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class CommodityViewModel
    {
        /// <summary>
        /// 物品属性
        /// </summary>
        public Commodity commodity { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 物品出入库时间
        /// </summary>
        public DateTime Out_into_date { get; set; }
    }
}