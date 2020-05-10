using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;
using 仓储系统.DataAccessLayer;

namespace 仓储系统.BusinessLayer
{
    public class InOutWarehouseBusinessLayer
    {
        //通过相应的storage表获得exist表
        public Exist GetExist(Storage storage)
        {
            Exist exist = new Exist();
            exist.IO_Id = storage.IO_Id;
            exist.Count = storage.Count;
            exist.IntoDate = DateTime.Now;
            exist.Co_id = storage.Co_id;

            OutIntoWareBusinessLayer outIntoWareBusinessLayer = new OutIntoWareBusinessLayer();
            Out_Into_ware out_Into_Ware = outIntoWareBusinessLayer.GetOut_Into_ware(storage.IO_Id);

            exist.W_id = out_Into_Ware.Ware_id;
            exist.U_id = out_Into_Ware.User_id;
            return exist;                           
        }
    }
}