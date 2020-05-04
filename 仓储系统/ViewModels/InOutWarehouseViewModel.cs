using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class InOutWarehouseViewModel : LayoutViewModel
    {
        //public enum MySelect 
        //{
        //    [Description("商品编号")]
        //    Co_Id,
        //    [Description("商品名称")]
        //    Co_name,
        //    [Description("条码编号")]
        //    Co_code_id
        //}
        //public MySelect mySelect { get; set; }

        public Commodity commodity { get; set; }


    }
}