using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓储系统.ViewModels
{
    public class StorageViewModel : LayoutViewModel
    {
        /// <summary>
        /// 信息的表显示的数据，包括单号、物品、处理人、系统
        /// </summary>
        public ExistTableListViewModel existTableListViewModel { get; set; }

    }
}