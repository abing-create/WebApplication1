using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 仓储系统.Models;

namespace 仓储系统.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Convert.ToInt32(filterContext.HttpContext.Session["level"]) != (Convert.ToInt32(level.Admin)))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "用户组暂无权限访问此页面."
                };
            }
        }
    }
}