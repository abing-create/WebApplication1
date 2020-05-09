using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓储系统.ViewModels
{
    public class SearchStorageViewModel
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime start_date { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime end_date { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string U_name { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string U_Id { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string W_name { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string Wa_Id { get; set; }

        /// <summary>
        /// 仓库位置
        /// </summary>
        public string Wa_address { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Co_name { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string Co_Id { get; set; }

        /// <summary>
        /// 条码编号
        /// </summary>
        public string Co_bar_code { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public string Co_type { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string Co_specification { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public string Co_price { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Co_unit { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public string Co_weight { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string IO_Id { get; set; }
    }
}