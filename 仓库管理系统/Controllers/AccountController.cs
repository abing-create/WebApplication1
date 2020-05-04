using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 仓库管理系统.DataAccessLayer;
using 仓库管理系统.ViewModels;

namespace 仓库管理系统.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //public ActionResult Login(FormCollection formcollection)
        //{
        //    string user = formcollection["inputEmail3"];
        //    string password = formcollection["inputPassword3"];

        //    LoginSqlData loginSqlData = new LoginSqlData();
        //    if(loginSqlData.myLogin(user, password))
        //        return RedirectToAction("InOutWarehouse", "Home");//跳转到首页
        //    return View();
        //}
        [HttpPost]
        public ActionResult DoLogin(LoginViewModel loginViewModel)
        {
            //string user = formcollection["inputEmail3"];
            //string password = formcollection["inputPassword3"];
           //// LoginSqlData loginSqlData = new LoginSqlData();
           // if (loginSqlData.myLogin(loginViewModel.U_name, loginViewModel.U_password))
           // {
           //     loginSqlData.saveRecord(loginViewModel.U_name);
           //     Session["User"] = loginViewModel.U_name;
           //     Session["Password"] = loginViewModel.U_password;
           //     return RedirectToAction("Information", "Home");//跳转到首页
           // }
            ModelState.AddModelError("CredentialError", "Invalid Username or Password");
            return View("Login");
        }
    }
}