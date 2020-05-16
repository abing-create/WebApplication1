using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;
using 仓储系统.DataAccessLayer;
using 仓储系统.ViewModels;
using PagedList;
using System.Configuration;

namespace 仓储系统.BusinessLayer
{
    public class InformationBusinessLayer
    {
        public InformationViewModel getInformationViewModel(string name, string passwrd, string select = "", string uname = "")
        {
            //个人信息页面视图模型
            InformationViewModel informationViewModel = new InformationViewModel();

            //用户信息
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();

            informationViewModel.createUserViewModel = new CreateUserViewModel();
            informationViewModel.createUserViewModel.user = userBusinessLayer.GetUser(name);//得到当前登录者的用户信息        

            //用户是管理者的时候，可以得到所有的用户信息
            if(select != "")
                informationViewModel.users = userBusinessLayer.GetUsers(select, uname);//得到指定条件的人
            else if (informationViewModel.createUserViewModel.user.U_level == level.Admin)
                informationViewModel.users = userBusinessLayer.GetUser();
            else
                informationViewModel.users = new List<User>();
            return informationViewModel;
        }

        /// <summary>
        /// 获取LoginRecord视图（表格）的model
        /// </summary>
        /// <param name="page"></param>
        /// <param name="isAdmin"></param>
        /// <param name="useName"></param>
        /// <returns></returns>
        public IPagedList<Record> GetPagedList(int? page, bool isAdmin, string useName)
        {
            RecordBusinessLayer recordBusinessLayer = new RecordBusinessLayer();
            List<Record> records;
            if (isAdmin)
                records = recordBusinessLayer.GetRecord();
            else
                records = recordBusinessLayer.GetRecord(useName);
            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
            //通过ToPagedList扩展方法进行分页
            IPagedList<Record> pagedList = records.ToPagedList(pageNumber, pageSize);
            return pagedList;
        }
    }
}