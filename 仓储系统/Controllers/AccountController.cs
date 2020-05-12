using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 仓储系统.ViewModels;
using 仓储系统.BusinessLayer;
using 仓储系统.DataAccessLayer;
using 仓储系统.Models;

namespace 仓储系统.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(LoginViewModel loginViewModel)
        {
            LoginBusinessLayer loginBusinessLayer = new LoginBusinessLayer();
            if (!loginBusinessLayer.IsFail(loginViewModel.U_name))
            {
                if (!loginBusinessLayer.IsLogin(loginViewModel.U_name, loginViewModel.U_password))
                {
                    ModelState.AddModelError("CredentialError", "账号或者密码错误");
                    return View("Login");
                }
                Session["User"] = loginViewModel.U_name;
                Session["Password"] = loginViewModel.U_password;
                Session["level"] = loginBusinessLayer.getLevel(loginViewModel.U_name);
                return RedirectToAction("Information", "Home");//跳转到首页
            }
            ModelState.AddModelError("CredentialError", "登录错误次数超过五次，请联系管理员修改密码");
            return View("Login");
        }

        [HttpGet]
        public ActionResult Registered()
        {
            return View("Registered");
        }
    }
}