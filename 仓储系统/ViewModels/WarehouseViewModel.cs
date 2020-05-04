using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class WarehouseViewModel : LayoutViewModel
    {
        public List<Warehouse> warehouses { get; set; }
    }
}