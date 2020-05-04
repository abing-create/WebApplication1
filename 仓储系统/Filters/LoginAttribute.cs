using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 仓储系统.Filters
{
    public class LoginAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //获取当前相对项目的更目录的路径
            string path = filterContext.HttpContext.Request.CurrentExecutionFilePath;
            if (!path.StartsWith("/Account/", StringComparison.CurrentCultureIgnoreCase))
            {
                //如果Session中不存在该值，那么表示没有登录
                if (filterContext.HttpContext.Session["User"] == null)
                {
                    filterContext.Result = new RedirectResult("/Account/Login");
                }
            }

        }
    }
}