using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{
    /// <summary>
    /// 员工登录信息表
    /// </summary>
    public class Record
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int U_id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [StringLength(20, MinimumLength = 5, ErrorMessage = "用户名必须大于5位数小于20位数")]
        public string U_name { get; set; }
        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime Record_date { get; set; }
    }
}