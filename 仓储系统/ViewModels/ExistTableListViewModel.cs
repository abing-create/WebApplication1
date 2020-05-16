using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓储系统.ViewModels
{
    public class ExistTableListViewModel
    {
        
        public List<ExistTableViewModel> existTableViewModels { get; set; }
        public IPagedList<ExistTableViewModel> iPagedLists { get; set; }


        /// <summary>
        /// 是否显示，用户等级为admin就为空，不是就是none
        /// </summary>
        public string Display { get; set; }
    }
}