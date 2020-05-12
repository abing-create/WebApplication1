using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{
    public struct CommodityMember
    {
        public string Co_Id;
        public string Co_name;
        public string Co_bar_code;
        public string Co_type;
        public string Co_specification;
        public string Co_price;
        public string Co_unit;
        public string Co_weight;
        public string Co_note;

        public void Clear()
        {
            Co_Id = null;
            Co_name = null;
            Co_bar_code = null;
            Co_type = null;
            Co_specification = null;
            Co_price = null;
            Co_unit = null;
            Co_weight = null;
            Co_note = null;
        }
    }

    /// <summary>
    /// 商品类，用来存储商品信息
    /// </summary>
    public class Commodity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [Index(IsUnique = true)]//唯一约束
        public int Co_Id { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Index(IsUnique = true)]//唯一约束
        [StringLength(50)]
        public string Co_name { get; set; }

        /// <summary>
        /// 条码编号
        /// </summary>
        [StringLength(100)]
        public string  Co_bar_code { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        [StringLength(50)]
        public string Co_type { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        [StringLength(50)]
        public string Co_specification { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public double Co_price { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [StringLength(20)]
        public string Co_unit { get; set; }

        /// <summary>
        /// 商品重量
        /// </summary>
        public double Co_weight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Co_note { get; set; }
    }

}