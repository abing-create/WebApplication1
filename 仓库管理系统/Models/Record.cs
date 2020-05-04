using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓库管理系统.Models
{
    public class Record
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int U_id { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string U_name { get; set; }
        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime Record_date { get; set; }
    }
}