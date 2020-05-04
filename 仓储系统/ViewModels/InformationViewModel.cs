using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class InformationViewModel : LayoutViewModel
    {
        public CreateUserViewModel createUserViewModel { get; set; }

        public List<User> users { get; set; }

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