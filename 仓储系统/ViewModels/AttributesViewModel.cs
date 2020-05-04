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
        public List<Commodity> commodities { get; set; }

        public Commodity commoditie { get; set; }
    }
}