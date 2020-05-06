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
        private IO_Type IO_Type;    //出库还是入库!
        public static level level;  //等级!
        private static string Table_Id;    //出入库表单号
        private int userId = 0;     //登录者编号!
        private int wareId = 0;     //仓库编号!

        [HttpGet]
        public ActionResult InOutWarehouse()
        {
            //Session["Table_Id"] = null;
            Table_Id = "";
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
            Session["Table_Id"] = null;
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
            else if (Sumbit == "出库")
            {
                out_Into_Ware.type = IO_Type.OUT;
                out_Into_Ware.Table_Id = "OUT" + Table_Id;
            }
            //Session["Table_Id"] = out_Into_Ware.Table_Id;
            Table_Id = out_Into_Ware.Table_Id;
            //存入数据库
            OutIntoWareBusinessLayer outIntoWareBusinessLayer = new OutIntoWareBusinessLayer();
            outIntoWareBusinessLayer.InsertOut_Into_ware(out_Into_Ware);

            InOutWarehouseViewModel inOutWarehouseViewModel = new InOutWarehouseViewModel();
            inOutWarehouseViewModel.UserName = Session["User"].ToString();
            inOutWarehouseViewModel.commodity = new Commodity();

            return View("inOutWarehouse", inOutWarehouseViewModel);
        }

        [HttpPost]
        public ActionResult InOutWarehouse(Commodity commodity, string sid, int Count)
        {
            InOutWarehouseViewModel inOutWarehouseViewModel = new InOutWarehouseViewModel();
            inOutWarehouseViewModel.UserName = Session["User"].ToString();
            if (Session["SelectIntoOut"] != null && Convert.ToBoolean(Session["SelectIntoOut"]))
            {
                //存储数据到Storage表(出入库单号、物品号、物品数量)
                StorageBusinessLayer storageBusinessLayer = new StorageBusinessLayer();
                Storage storage = new Storage();
                storage.Co_id = commodity.Co_Id;
                //storage.IO_Id = Session["Table_Id"].ToString();
                storage.IO_Id = Table_Id;
                storage.Count = Count;
                storage.IntoDate = DateTime.Now;
                storageBusinessLayer.InsertStorage(storage);

                Session["SelectIntoOut"] = false;
                inOutWarehouseViewModel.commodity = commodity;
            }
            else
            {
                //显示数据
                CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
                inOutWarehouseViewModel.commodity = commodityBusinessLayer.GetCommodity(sid, commodity);
                if (inOutWarehouseViewModel.commodity != null)
                {
                    Session["SelectIntoOut"] = true;
                }
            }

            return View("inOutWarehouse", inOutWarehouseViewModel);
        }

        public ActionResult InOutTable()
        {
            //if (Session["Table_Id"] == null)
            if(Table_Id == "")
            {
                //没有创建表单号
                return new EmptyResult();
            } 
            //获取信息
            InOutTableViewModel inOutTableViewModel = new InOutTableViewModel();
            inOutTableViewModel.type = IO_Type == IO_Type.INTO ? "入库" : "出库";
            inOutTableViewModel.commodityViewModels = new List<CommodityViewModel>();

            //通过表号查询storate表的数据(物品编号，数量，时间)，
            //通过编号查询commodity表
            //将数据(commodity表、时间、数量)存入CommodityViewModel
            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            Commodity commodity;
            StorageBusinessLayer storageBusinessLayer = new StorageBusinessLayer();
            //List<Storage> storages = storageBusinessLayer.GetStorage("IO_Id", Session["Table_Id"].ToString());
            List<Storage> storages = storageBusinessLayer.GetStorage("IO_Id", Table_Id);
            foreach (Storage storage in storages)
            {
                commodity = commodityBusinessLayer.GetCommodity(storage.Co_id, "", "");
                inOutTableViewModel.commodityViewModels.Add(new CommodityViewModel() { commodity = commodity, Count = storage.Count, Out_into_date = DateTime.Now });
            }

            return PartialView("InOutTable", inOutTableViewModel);
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

        //[HttpGet]
        public ActionResult InformationAdminButton()
        {
            if (level.Admin == (level)Session["level"])
            {
                Session["UserButton"] = false;
                return PartialView("InformationAdminButton");
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
                Session["UserButton"] = !Convert.ToBoolean(Session["UserButton"]);
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
        public ActionResult UpdataUser(User user, string BtnSubmit)
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if (BtnSubmit == "修改")
            {
                //userBusinessLayer.InsertUser(user);
                userBusinessLayer.UpdataUsers(Session["User"].ToString(), user);
                return RedirectToAction("Information");
            }
            else if (BtnSubmit == "取消")
            {

                return RedirectToAction("Information");
            }
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            createUserViewModel.user = userBusinessLayer.GetUser(Session["User"].ToString());
            return PartialView("UpdataUser", createUserViewModel);

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
                foreach (User Iuser in users)
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
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if (BtnSubmit == "保存")
            {
                userBusinessLayer.UpdataUsers(Session["User"].ToString(), user);
                return RedirectToAction("Information");
            }
            else if (BtnSubmit == "取消")
            {
                return RedirectToAction("Information");
            }

            CreateUserViewModel v = new CreateUserViewModel();
            v.user = userBusinessLayer.GetUser(Session["User"].ToString());
            return PartialView("CreateUser", v);
        }

        [HttpPost]
        public ActionResult AddUser(User user, string BtnSubmit)
        {
            //CreateUserViewModel v = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if (BtnSubmit == "保存")
            {
                userBusinessLayer.InsertUser(user);
                //userBusinessLayer.UpdataUsers(Session["User"].ToString(), user);
                //userBusinessLayer.InsertUser(user);
            }
            else if (BtnSubmit == "取消")
            {

            }


            return RedirectToAction("Information");
        }

        public ActionResult AddNew()
        {
            CreateUserViewModel v = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            v.user = userBusinessLayer.GetUser(Session["User"].ToString());

            return PartialView("CreateUser", v);
        }

        [HttpGet]
        public ActionResult Attributes()
        {
            AttributesViewModel attributesViewModel = new AttributesViewModel();
            attributesViewModel.UserName = Session["User"].ToString();

            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            attributesViewModel.commodities = commodityBusinessLayer.GetCommodity();

            attributesViewModel.commoditie = new Commodity();

            return View("Attributes", attributesViewModel);
        }

        /// <summary>
        /// 搜索物品功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult searchAttrbutetes(string Select, string name, string BtnSubmit)
        {
            //如果BtnSubmit是触发的搜索按键
            AttributesViewModel attributesViewModel = new AttributesViewModel();
            attributesViewModel.UserName = Session["User"].ToString();//继承的，显示右边的用户名

            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            attributesViewModel.commodities = commodityBusinessLayer.GetCommodity(Select, name);

            attributesViewModel.commoditie = new Commodity();

            return View("Attributes", attributesViewModel);
        }

        /// <summary>
        /// 添加物品的功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCommodity(Commodity model, string BtnSubmit)
        {
            //如果是按键操作，返回重定向
            CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
            if (BtnSubmit == "添加")
            {
                commodityBusinessLayer.InsertCommodity(model);
                return RedirectToAction("Attributes");
            }
            else if (BtnSubmit == "提交更改")
            {
                
            }

            //如果不是按键操作，刷新本页面
            return PartialView("CreateCommodity"); 
        }


        public ActionResult Storage()
        {
            StorageViewModel storageViewModel = new StorageViewModel();
            storageViewModel.UserName = Session["User"].ToString();
            return View("Storage", storageViewModel);
        }

        //[AdminFilter]
        public ActionResult Warehouse()
        {
            WarehouseViewModel warehouseViewModel = new WarehouseViewModel();
            //判断是否是管理人员，是则有权限进行修改
            if(Convert.ToInt32(Session["level"]) == Convert.ToInt32(level.Admin))
                warehouseViewModel.display = "";
            else
                warehouseViewModel.display = "display:none";

            warehouseViewModel.UserName = Session["User"].ToString();

            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
            warehouseViewModel.warehouses = warehouseBusinessLayer.GetWarehouse();

            return View("Warehouse", warehouseViewModel);
        }

        [HttpPost]
        public ActionResult searchWarehouse(string wareName)
        {
            WarehouseViewModel warehouseViewModel = new WarehouseViewModel();
            //判断是否是管理人员，是则有权限进行修改
            if (Convert.ToInt32(Session["level"]) == Convert.ToInt32(level.Admin))
                warehouseViewModel.display = "";
            else
                warehouseViewModel.display = "display:none";

            warehouseViewModel.UserName = Session["User"].ToString();

            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
            warehouseViewModel.warehouses = warehouseBusinessLayer.GetWarehouse(wareName);

            return View("Warehouse", warehouseViewModel);
        }

        /// <summary>
        /// 添加仓库的页面
        /// </summary>
        /// <returns></returns>
        //public ActionResult CreateWarehouse()
        //{
        //    CreateWarehouseViewModel createWarehouseViewModel = new CreateWarehouseViewModel();
        //    createWarehouseViewModel.warehouse = new Warehouse();

        //    return PartialView("CreateWarehouse", createWarehouseViewModel);
        //}

        /// <summary>
        /// 添加仓库页面的操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BtnSubmit"></param>
        /// <returns></returns>
        [HttpPost]
        [AdminFilter]
        public ActionResult SaveWarehouse(Warehouse model, string BtnSubmit)
        {
            //如果是按键操作，返回重定向
            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
            if (BtnSubmit == "添加")
            {
                warehouseBusinessLayer.InsertWarehouse(model);
                return RedirectToAction("Warehouse");
            }
            else if (BtnSubmit == "提交更改")
            {
                warehouseBusinessLayer.InputWarehouse(model.Wa_name, model);
                return RedirectToAction("Warehouse");
            }

            //如果不是按键操作，刷新本页面
            CreateWarehouseViewModel createWarehouseViewModel = new CreateWarehouseViewModel();
            createWarehouseViewModel.warehouse = new Warehouse();
            return PartialView("CreateWarehouse", createWarehouseViewModel); ;
        }

        [HttpPost]
        [AdminFilter]
        public ActionResult DeleteWarehouse(string Wa_name, string BtnSubmit)
        {
            if (BtnSubmit == "确定")
            {
                WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
                warehouseBusinessLayer.DeleteWarehouse(Wa_name);
                return RedirectToAction("Warehouse");
            }
            else if (BtnSubmit == "取消")
            {
                return RedirectToAction("Warehouse");
            }

            //如果不是按键操作，刷新本页面
            CreateWarehouseViewModel createWarehouseViewModel = new CreateWarehouseViewModel();
            createWarehouseViewModel.warehouse = new Warehouse();
            return PartialView("CreateWarehouse", createWarehouseViewModel); ;
        }
    }
}