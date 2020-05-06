using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class InformationViewModel : LayoutViewModel
    {
        /// <summary>
        /// 创建用户、更改个人信息用的ViewModel,登录者的用户信息存放在其中
        /// </summary>
        public CreateUserViewModel createUserViewModel { get; set; }

        public List<User> users { get; set; }
    }
}