using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 弹出.Models;

namespace 弹出.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetInfo(Person person)
        {
            return PartialView("_DisplayJavaScriptObject", person);
        }
    }
}