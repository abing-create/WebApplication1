using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 仓储系统.ViewModels;
using 仓储系统.DataAccessLayer;
using 仓储系统.Models;
using 仓储系统.BusinessLayer;
using 仓储系统.Filters;

namespace 仓储系统.Controllers
{
    [Login]
    public class HomeController : Controller
    {
        private IO_Type IO_Type;
        public static level level;  //等级!
        private string Table_Id;    //出入库表单号
        private int userId = 0;     //登录者编号!
        private int wareId = 0;     //仓库编号!

        [HttpGet]
        public ActionResult InOutWarehouse()
        {
            InOutWarehouseViewModel inOutWarehouseViewModel = new InOutWarehouseViewModel();
            inOutWarehouseViewModel.UserName = Session["User"].ToString();
            inOutWarehouseViewModel.commodity = new Commodity();
            //inOutWarehouseViewModel.mySelect = InOutWarehouseViewModel.MySelect.Co_Id;
            return View("inOutWarehouse", inOutWarehouseViewModel);
        }

        [HttpPost]
        //[MultiButton("入库")]
        public ActionResult MakeTableSubmit(string Sumbit)
        {
            //string Sumbit = "入库";
            Out_Into_ware out_Into_Ware = new Out_Into_ware();
            out_Into_Ware.Make_date = DateTime.Now;//时间
            out_Into_Ware.User_id = userId;//负责人编号
            out_Into_Ware.Ware_id = wareId;//仓库编号
            //创建表单号
            Table_Id = "12345";
            if (Sumbit == "入库")
            {
                out_Into_Ware.type = IO_Type.INTO;
                out_Into_Ware.Table_Id = "INTO" + Table_Id;
            }
            else if(Sumbit == "出库")
            {
                out_Into_Ware.type = IO_Type.OUT;
                out_Into_Ware.Table_Id = "OUT" + Table_Id;
            }
            //存入数据库
            OutIntoWareBusinessLayer outIntoWareBusinessLayer = new OutIntoWareBusinessLayer();
            outIntoWareBusinessLayer.InsertOut_Into_ware(out_Into_Ware);

            InOutWarehouseViewModel inOutWarehouseViewModel = new InOutWarehouseViewModel();
            inOutWarehouseViewModel.UserName = Session["User"].ToString();
            inOutWarehouseViewModel.commodity = new Commodity();

            return View("inOutWarehouse", inOutWarehouseViewModel);
        }

        [HttpPost]
        public ActionResult InOutWarehouse(Commodity commodity, string sid)
        {
            InOutWarehouseViewModel inOutWarehouseViewModel = new InOutWarehouseViewModel();
            inOutWarehouseViewModel.UserName = Session["User"].ToString();
            //inOutWarehouseViewModel.commodity = new Commodity();

            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            inOutWarehouseViewModel.commodity = commodityBusinessLayer.GetCommodity(sid, commodity);

            return View("inOutWarehouse", inOutWarehouseViewModel);
        }

        public ActionResult InOutTable()
        {
            if (false)
            {
                InOutTableViewModel inOutTableViewModel = new InOutTableViewModel();
                return PartialView("InOutTable", inOutTableViewModel);
            }
            return new EmptyResult();
        }

        public ActionResult Information()
        {
            Session["LoginRecord"] = false;

            //个人信息页面视图模型
            InformationBusinessLayer informationBusinessLayer = new InformationBusinessLayer();
            string name = Session["User"].ToString();
            string password = Session["Password"].ToString();
            InformationViewModel informationViewModel = informationBusinessLayer.getInformationViewModel(name, password);

            return View("Information", informationViewModel);
        }

        [HttpPost]
        public ActionResult Information(string select)
        {
            switch (select)
            {
                case "查询记录":
                    Session["LoginRecord"] = !Convert.ToBoolean(Session["LoginRecord"]);
                    break;
                case "修改信息":
                    Session["LoginRecord"] = false;
                    break;
            }

            //个人信息页面视图模型
            InformationBusinessLayer informationBusinessLayer = new InformationBusinessLayer();
            string name = Session["User"].ToString();
            string password = Session["Password"].ToString();
            InformationViewModel informationViewModel = informationBusinessLayer.getInformationViewModel(name, password);

            return View("Information", informationViewModel);
        }

        [HttpGet]
        public ActionResult InformationAdmin()
        {
            if (level.Admin == (level)Session["level"])
            {
                Session["UserTable"] = false;
                return PartialView("InformationAdmin");
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult InformationAdmin(string select)
        {
            if (level.Admin != (level)Session["level"])
            {
                return new EmptyResult();
            }
            if (select == "查看用户")
            {
                Session["UserTable"] = !Convert.ToBoolean(Session["UserTable"]);
            }
            return PartialView("InformationAdmin");
        }

        public ActionResult Test()
        {
            //return PartialView(); 
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            createUserViewModel.user = userBusinessLayer.GetUser(Session["User"].ToString());
            return PartialView("Test", createUserViewModel);
            
        }

        public ActionResult UpdataUser()
        {
            //return PartialView(); 
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            createUserViewModel.user = userBusinessLayer.GetUser(Session["User"].ToString());
            return PartialView("UpdataUser", createUserViewModel);
        }

        [HttpPost]
        public void UpdataUser(User user, string BtnSubmit)
        {
            if (BtnSubmit == "保存")
            {
                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                userBusinessLayer.UpdataUsers(Session["User"].ToString(), user);
            }
        }

        public ActionResult LoginRecord()
        {
            if (Convert.ToBoolean(Session["LoginRecord"]))
            {
                RecordListViewModel recordListViewModel = new RecordListViewModel();
                RecordBusinessLayer recordBusinessLayer = new RecordBusinessLayer();
                recordListViewModel.records = new List<RecordViewModel>();
                List<Record> records;
                if ((level)Session["level"] == level.Admin)
                    records = recordBusinessLayer.GetRecord();
                else
                    records = recordBusinessLayer.GetRecord(Session["User"].ToString());
                foreach (Record Irecord in records)
                {
                    recordListViewModel.records.Add(new RecordViewModel(Irecord));
                }
                return PartialView("LoginRecord", recordListViewModel);
            }
            return new EmptyResult();
        }

        public ActionResult UserTable()
        {
            if (Convert.ToBoolean(Session["UserTable"]))
            {
                UserListViewModel userListViewModel = new UserListViewModel();
                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                userListViewModel.users = new List<UserViewModel>();
                List<User> users = userBusinessLayer.GetUser();
                foreach(User Iuser in users)
                {
                    userListViewModel.users.Add(new UserViewModel() { user = Iuser });
                }

                return PartialView("UserTable", userListViewModel);
            }
            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult GetAddUser()
        {
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            if (true)//Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("CreateUser", createUserViewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        public ActionResult SaveUser(User user, string BtnSubmit)
        {
            //CreateUserViewModel v = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            //if (BtnSubmit == "保存")
            //{
            //    userBusinessLayer.InsertUser(user);
            //}
            //else if (BtnSubmit == "取消")
            //{
            //}
            //userBusinessLayer.InsertUser(user);

            userBusinessLayer.UpdataUsers(Session["User"].ToString(), user);

            //v.user = userBusinessLayer.GetUser(Session["User"].ToString());
            //return PartialView("CreateUser", v);
            return RedirectToAction("Information");
        }

        public ActionResult AddNew()
        {
            CreateUserViewModel v = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            v.user = userBusinessLayer.GetUser(Session["User"].ToString());

            return PartialView("CreateUser", v);
        }

        public ActionResult Attributes()
        {
            AttributesViewModel attributesViewModel = new AttributesViewModel();
            attributesViewModel.UserName = Session["User"].ToString();

            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            attributesViewModel.commodities = commodityBusinessLayer.GetCommodity();

            attributesViewModel.commoditie = new Commodity();

            return View("Attributes", attributesViewModel);
        }
        public ActionResult Storage()
        {
            StorageViewModel storageViewModel = new StorageViewModel();
            storageViewModel.UserName = Session["User"].ToString();
            return View("Storage", storageViewModel);
        }
        public ActionResult Warehouse()
        {
            WarehouseViewModel warehouseViewModel = new WarehouseViewModel();
            warehouseViewModel.UserName = Session["User"].ToString();

            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
            warehouseViewModel.warehouses = warehouseBusinessLayer.GetWarehouse();

            return View("Warehouse", warehouseViewModel);
        }

        public ActionResult CreateWarehouse()
        {
            CreateWarehouseViewModel createWarehouseViewModel = new CreateWarehouseViewModel();
            createWarehouseViewModel.warehouse = new Warehouse();

            return PartialView("CreateWarehouse", createWarehouseViewModel);
        }

        [HttpPost]
        public ActionResult SaveWarehouse(CreateWarehouseViewModel model, string BtnSubmit)
        {
            CreateWarehouseViewModel createWarehouseViewModel = new CreateWarehouseViewModel();
            createWarehouseViewModel.warehouse = new Warehouse();

            if (BtnSubmit == "保存")
            {
                WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
                warehouseBusinessLayer.InsertWarehouse(model.warehouse);
            }
            else if (BtnSubmit == "取消")
            {

            }

            return PartialView("CreateWarehouse", createWarehouseViewModel); ;
        }
    }
}