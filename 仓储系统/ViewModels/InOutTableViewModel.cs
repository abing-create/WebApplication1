using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class InOutTableViewModel
    {
        /// <summary>
        /// 物品及其数量
        /// </summary>
        public List<CommodityViewModel>  commodityViewModels { get; set; }

        /// <summary>
        /// 出入库表单
        /// </summary>
        //public Out_Into_ware out_Into_ware { get; set; }

        /// <summary>
        /// 出库还是入库
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string IO_id { get; set; }
    }
}