using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 仓库管理系统.Models
{
    public class User
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int U_Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string U_name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string U_password { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string U_post { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string U_department { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime U_birthday { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string U_phone { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public char U_level { get; set; }

        /// <summary>
        /// 密码提示
        /// </summary>
        public string U_point { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string U_sex { get; set; }
    }
}