using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{
    /// <summary>
    /// 存储表
    /// </summary>
    public class Storage
    {
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 出入库单号
        /// </summary>
        [StringLength(50)]
        public string IO_Id { get; set; }

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