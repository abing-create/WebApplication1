using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class WarehouseViewModel : LayoutViewModel
    {
        public IPagedList<Warehouse> warehouses { get; set; }

        public string display { get; set; }
    }
}