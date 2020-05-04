using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 弹出.Models;

namespace 弹出.Content
{
    public class GooController : Controller
    {
        // GET: Goo
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


    }
}