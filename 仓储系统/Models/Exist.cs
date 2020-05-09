using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{
    public struct ExistMember
    {
        public string IO_Id;
        public string U_id;
        public string W_id;
        public string Co_id;
        public string Count;
        //时间区间
        public string Star_date;
        public string End_date;

        public void Clear()
        {
            IO_Id = null;
            U_id = null;
            W_id = null;
            Co_id = null;
            Count = null;
            Star_date = null;
            End_date = null;
        }
    }


    public class Exist
    {
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 入库单号
        /// </summary>
        [StringLength(50)]
        public string IO_Id { get; set; }

        /// <summary>
        /// 入库人编号
        /// </summary>
        public int U_id { get; set; }

        /// <summary>
        /// 入库仓库编号
        /// </summary>
        public int W_id { get; set; }

        /// <summary>
        /// 物品编号
        /// </summary>
        public int Co_id { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime IntoDate { get; set; }
    }
}