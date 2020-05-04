using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
{
    public class RecordBusinessLayer
    {
        /// <summary>
        /// 查询员工登录信息
        /// </summary>
        /// <returns></returns>
        public List<Record> GetRecord()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.records.ToList();
        }

        public List<Record> GetRecord(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<Record> newRecords = new List<Record>();
            var models = dB.records.Where(c => c.U_name == name);
            return models.ToList();
        }

        /// <summary>
        /// 存储员工登录信息
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public Record InsertRecord(Record record)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.records.Add(record);
            dB.SaveChanges();
            return record;
        }
    }
}