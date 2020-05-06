using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓储系统.ViewModels
{
    public class DeleteViewModel
    {
        /// <summary>
        /// 要删除的名字
        /// </summary>
        public string D_name { get; set; }

        /// <summary>
        /// 要删除的id
        /// </summary>
        public string D_id { get; set; }

        /// <summary>
        /// 要删除的表名
        /// </summary>
        public string T_name { get; set; }
    }
}