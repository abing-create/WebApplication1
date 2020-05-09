using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{

    public struct WarehouseMember
    {
        public string Wa_Id;
        public string Wa_name;
        public string Wa_address;
        public string Wa_princiopal;
        public string Wa_contact;
        public string Wa_capacity;
        public string Wa_able_capacity;

        public void Clear()
        {
            Wa_Id = null;
            Wa_name = null;
            Wa_address = null;
            Wa_princiopal = null;
            Wa_contact = null;
            Wa_capacity = null;
            Wa_able_capacity = null;
        }
    }

    /// <summary>
    /// 仓库表
    /// </summary>
    public class Warehouse
    {
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public int Wa_Id { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [StringLength(50)]
        public string Wa_name { get; set; }

        /// <summary>
        /// 仓库地址
        /// </summary>
        [StringLength(100)]
        public string Wa_address { get; set; }

        /// <summary>
        /// 仓库负责人
        /// </summary>
        [StringLength(20, MinimumLength = 5)]
        public string Wa_princiopal { get; set; }

        /// <summary>
        /// 仓库联系方式
        /// </summary>
        [StringLength(20)]
        public string Wa_contact { get; set; }

        /// <summary>
        /// 仓库容量
        /// </summary>
        [StringLength(50)]
        public string Wa_capacity { get; set; }

        /// <summary>
        /// 可用容量
        /// </summary>
        [StringLength(50)]
        public string Wa_able_capacity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Wa_note { get; set; }
    }
}