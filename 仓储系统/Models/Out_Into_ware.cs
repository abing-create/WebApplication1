using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{
    public enum IO_Type 
    {
        OUT,//入库
        INTO//出库
    }

    public class Out_Into_ware
    {

        [Key]
        public int id { get; set; }

        /// <summary>
        /// 出入库单号
        /// </summary>
        public string Table_Id { get; set; }

        /// <summary>
        /// 出入库表制作时间
        /// </summary>
        public DateTime Make_date { get; set; }

        /// <summary>
        /// 出入库仓库编号
        /// </summary>
        public int Ware_id { get; set; }

        /// <summary>
        /// 出入库类别
        /// </summary>
        public IO_Type type { get; set; }

        /// <summary>
        /// 出入库人编号
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}