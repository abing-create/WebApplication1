using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 弹出.Models;

namespace 弹出.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CostomViewModel costomViewModel = new CostomViewModel();
            costomViewModel.CustomerId = "abign";
            costomViewModel.CompanyName = "ggfgg";
            return View("Index", costomViewModel);
        }

        public ActionResult Test()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult About(string CustomerId)
        {
            ViewBag.Message = CustomerId;

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}