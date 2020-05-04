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
            informationViewModel.UserName = name;
            informationViewModel.Admin_password = passwrd;

            //用户信息
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            informationViewModel.users = userBusinessLayer.GetUser();

            informationViewModel.createUserViewModel = new CreateUserViewModel();
            return informationViewModel;
        }
    }
}