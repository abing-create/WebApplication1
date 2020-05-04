using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓库管理系统.Models;

namespace 仓库管理系统.ViewModels
{
    public class InformationViewModel : LayoutViewModel
    {
        /// <summary>
        /// 用户登录日期记录列表
        /// </summary>
        public List<RecordViewModel> record { get; set; }

        /// <summary>
        /// 被查看的用户信息
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// 登录者的密码
        /// </summary>
        public string Admin_password { get; set; }
    }
}