using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;
using 仓储系统.DataAccessLayer;
using 仓储系统.ViewModels;

namespace 仓储系统.BusinessLayer
{
    public class InformationBusinessLayer
    {
        public InformationViewModel getInformationViewModel(string name, string passwrd)
        {
            //个人信息页面视图模型
            InformationViewModel informationViewModel = new InformationViewModel();

            //用户信息
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();

            informationViewModel.createUserViewModel = new CreateUserViewModel();
            informationViewModel.createUserViewModel.user = userBusinessLayer.GetUser(name);//得到当前登录者的用户信息        

            //用户是管理者的时候，可以得到所有的用户信息
            if (informationViewModel.createUserViewModel.user.U_level == level.Admin)
                informationViewModel.users = userBusinessLayer.GetUser();
            else
                informationViewModel.users = new List<User>();
            return informationViewModel;
        }
    }
}