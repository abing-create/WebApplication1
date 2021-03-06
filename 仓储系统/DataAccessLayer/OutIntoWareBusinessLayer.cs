﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class OutIntoWareBusinessLayer
    {
        /// <summary>
        /// 查询数据库TblOut_Into_ware表
        /// </summary>
        /// <returns></returns>
        public List<Out_Into_ware> GetOut_Into_ware()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.out_into_wares.ToList();
        }

        /// <summary>
        /// 根据入库表单号得到数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Out_Into_ware GetOut_Into_ware(string id)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var outIntoWare = dB.out_into_wares.Where(c => c.Table_Id == id).FirstOrDefault();
            if (outIntoWare != null)
            {
                return outIntoWare;
            }
            return (new Out_Into_ware());
        }

        public string GetMaxTable(IO_Type iO_Type)
        {
            string MaxTable;
            if(iO_Type == IO_Type.INTO)
            {
                MaxTable = "INTO";
            }
            else
            {
                MaxTable = "OUT";
            }
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            MaxTable += System.Text.RegularExpressions.Regex.Replace(time, "[-]", "");

            WarehouseERPDAL dB = new WarehouseERPDAL();
            var models = dB.out_into_wares.Where(c => c.type == iO_Type).ToList();
            if(models == null || models.Count == 0)
            {
                MaxTable += "0000";
            }
            else 
            {
                var id = models.Max(t => t.id);
                var model = dB.out_into_wares.Where(c => c.id == id).FirstOrDefault();
                if (model.Make_date.Subtract(DateTime.Now).Days != 0)
                {
                    MaxTable += "0000";
                }
                string str = model.Table_Id.Substring(model.Table_Id.Length - 4, 4);
                int count = Convert.ToInt32(str);
                count += 1;
                str = count.ToString();
                count = 4 - str.Length;
                while(count-- > 0)
                {
                    str = "0" + str;
                }
                MaxTable += str;
            }            

            return MaxTable;
        }

        /// <summary>
        /// 插入数据到表TblOut_Into_ware
        /// </summary>
        /// <param name="outIntoWare">插入的数据</param>
        /// <returns></returns>
        public Out_Into_ware InsertOut_Into_ware(Out_Into_ware outIntoWare)
        {
            //dB.out_into_wares.Add(outIntoWare);
            //dB.SaveChanges();
            //return outIntoWare;

            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.out_into_wares.Add(outIntoWare);
            dB.SaveChanges();
            return outIntoWare;
        }

        /// <summary>
        /// 修改数据到表TblOut_Into_ware
        /// </summary>
        /// <param name="name">Out_Into_ware的U_nam名字</param>
        /// <param name="outIntoWare">更新成的表</param>
        public void UpdataOut_Into_wares(string id, Out_Into_ware outIntoWare)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.out_into_wares.Where(c => c.Table_Id == id).FirstOrDefault();
            if (model != null)
            {
                model = outIntoWare;
            }
        }

        /// <summary>
        /// 插入多个数据到表TblOut_Into_ware
        /// </summary>
        /// <param name="out_into_wares"></param>
        public void InsertOut_Into_wares(List<Out_Into_ware> out_into_wares)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.out_into_wares.AddRange(out_into_wares);
            dB.SaveChanges();
        }

        public void Delete(string Table_Id)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.out_into_wares.Where(c => c.Table_Id == Table_Id).FirstOrDefault();
            dB.out_into_wares.Remove(model);
            dB.SaveChanges();
        }
    }
}