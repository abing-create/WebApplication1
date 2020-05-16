using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            
            return View();
        }

        public ActionResult People(int? page)
        {
            List<Person> people = new List<Person>();
            for(int i = 0; i  < 50; i++)
            {
                people.Add(new Person() {name = "user", age = i });
            }
            //people = people.OrderBy(x => x.age);

            //第几页
            int pageNumber = page ?? 1;
            //每页显示多少条
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);            
            //通过ToPagedList扩展方法进行分页
            IPagedList<Person> pagedList = people.ToPagedList(pageNumber, pageSize);

            //将分页处理后的列表传给View
            return PartialView(pagedList);
        }


    }
}