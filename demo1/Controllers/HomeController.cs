using demo1.DataAccessLayer;
using demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataERPDAL dataERPDAL = new DataERPDAL();
            List<Employee> employee = new List<Employee>();
            employee = dataERPDAL.Employees.ToList();

            Class1ERPDAL class1ERPDAL = new Class1ERPDAL();
            List<Class1> class1s = new List<Class1>();
            class1s = class1ERPDAL.Class1.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}