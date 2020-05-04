using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 仓库管理系统.ViewModels;

namespace 仓库管理系统.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult InOutWarehouse()
        {
            return View();
        }

        public ActionResult Information()
        {
            //个人信息页面视图模型
            InformationViewModel informationViewModel = new InformationViewModel();
            informationViewModel.UserName = Session["User"].ToString();
            informationViewModel.Admin_password = Session["Password"].ToString();

            //InformationSqlData informationSqlData = new InformationSqlData();
            //个人信息视图模型
            //informationViewModel.user = informationSqlData.getUser(informationViewModel.UserName);

            //登录记录视图模型
            string userName = informationViewModel.UserName;
            //informationViewModel.record = informationSqlData.getRecord(userName);

            return View("Information", informationViewModel);
        }

        public ActionResult Attributes()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Storage()
        {
            return View();
        }
        public ActionResult Warehouse()
        {
            return View();
        }


    }
}