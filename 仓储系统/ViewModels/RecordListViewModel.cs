using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓储系统.ViewModels
{
    public class RecordListViewModel
    {
        /// <summary>
        /// 用户登录日期记录列表
        /// </summary>
        public List<RecordViewModel> records { get; set; }
    }
}