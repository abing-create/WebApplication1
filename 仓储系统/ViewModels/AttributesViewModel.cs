using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    /// <summary>
    /// 物品设置
    /// </summary>
    public class AttributesViewModel : LayoutViewModel
    {
       // public IPagedList<Commodity> commodities { get; set; }

        public IPagedList<CommPathViewModel> commPathViewModels { get; set; }

        public Commodity commoditie { get; set; }

        public bool IsSearch { get; set; }

        //public string path { get; set; }
    }
}