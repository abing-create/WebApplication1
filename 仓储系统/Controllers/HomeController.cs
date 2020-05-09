﻿using System;
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
        #region 全局变量
        private IO_Type IO_Type;    //出库还是入库!
        public static level level;  //等级!
        private static string Table_Id;    //出入库表单号
        private static int userId = 0;     //登录者编号!
        private static int wareId = 0;     //仓库编号!

        //三个变量用来保存用户是否搜索用户的信息
        private static string S_select = "";
        private static string S_name = "";
        private static bool IsSearchPeople = false;

        //存储界面搜索的信息
        private static UserMember S_userMember;               //用户表
        private static WarehouseMember S_warehouseMember;     //仓库表
        private static ExistMember S_existMember;             //在存表
        private static CommodityMember S_commodityMember;     //商品表
        private static bool IsSearchExist = false;

        #endregion

        #region 入库界面
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
#endregion

        #region 用户信息
        public ActionResult Information()
        {
            //Session["LoginRecord"] = false;

            //个人信息页面视图模型
            InformationBusinessLayer informationBusinessLayer = new InformationBusinessLayer();
            string name = Session["User"].ToString();
            string password = Session["Password"].ToString();
            InformationViewModel informationViewModel = informationBusinessLayer.getInformationViewModel(name, password);
            //全局变量用户组
            //S_users = informationViewModel.users;

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
                //if (Session["UserTable"] != null)
                //    Session["UserTable"] = !Convert.ToBoolean(Session["UserTable"]);
                //else
                //    Session["UserTable"] = false;
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
                IsSearchPeople = false;
                Session["UserTable"] = !Convert.ToBoolean(Session["UserTable"]);
                Session["UserButton"] = !Convert.ToBoolean(Session["UserButton"]);
            }
            return PartialView("InformationAdmin");
        }

        //public ActionResult Test()
        //{
        //    //return PartialView(); 
        //    CreateUserViewModel createUserViewModel = new CreateUserViewModel();
        //    UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
        //    createUserViewModel.user = userBusinessLayer.GetUser(Session["User"].ToString());
        //    return PartialView("Test", createUserViewModel);

        //}searchUser

        [HttpPost]
        public ActionResult searchUser(string Select, string uname, string BtnSubmit)
        {
            //如果BtnSubmit是触发的搜索按键
            AttributesViewModel attributesViewModel = new AttributesViewModel();
            attributesViewModel.UserName = Session["User"].ToString();//继承的，显示右边的用户名

            //个人信息页面视图模型
            InformationBusinessLayer informationBusinessLayer = new InformationBusinessLayer();
            string name = Session["User"].ToString();
            string password = Session["Password"].ToString();
            InformationViewModel informationViewModel = informationBusinessLayer.getInformationViewModel(name, password, Select, uname);

            S_select = Select;
            S_name = uname;
            IsSearchPeople = true;

            //S_users = informationViewModel.users;

            return View("Information", informationViewModel);
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
            //给Session["UserTable"]取反，使重定向后依然能够显示usertable
            //Session["UserTable"] = !Convert.ToBoolean(Session["UserTable"]);
            //Session["LoginRecord"] = !Convert.ToBoolean(Session["LoginRecord"]);
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if (BtnSubmit == "提交更改")
            {
                //userBusinessLayer.InsertUser(user);
                userBusinessLayer.UpdataUsers(Session["User"].ToString(), user);
                return RedirectToAction("Information");
            }
            else if (BtnSubmit == "取消")
            {

                return RedirectToAction("Information");
            }

            //UpdataUser不需要控制器来决定，这段代码没用
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();
            createUserViewModel.user = userBusinessLayer.GetUser(Session["User"].ToString());
            return PartialView("UpdataUser", createUserViewModel);

        }

        public ActionResult LoginRecord()
        {
            //如果Session["LoginRecord"]为true,则显示usertable,每次都触发information都给改变Session["LoginRecord"]取反
            if (Session["LoginRecord"] != null && Convert.ToBoolean(Session["LoginRecord"]))
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
            Session["LoginRecord"] = false;
            return new EmptyResult();
        }

        public ActionResult UserTable()
        {
            //如果Session["UserTable"]为true,则显示usertable,每次都触发information都给改变Session["UserTable"]取反
            if (Session["UserTable"] != null && Convert.ToBoolean(Session["UserTable"]))
            {
                //UserListViewModel userListViewModel = new UserListViewModel();
                //UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                //userListViewModel.users = userBusinessLayer.GetUser();

                UserListViewModel userListViewModel = new UserListViewModel();
                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                if(IsSearchPeople)
                    userListViewModel.users = userBusinessLayer.GetUsers(S_select, S_name);//得到指定条件的人;
                else
                    userListViewModel.users = userBusinessLayer.GetUser();

                return PartialView("UserTable", userListViewModel);
            }
            Session["UserTable"] = false;
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

        /// <summary>
        /// 管理员添加用户或则修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="BtnSubmit"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUser(User user, string BtnSubmit)
        {
            
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if (BtnSubmit == "提交更改")
            {
                userBusinessLayer.UpdataUsers(user.U_name, user);
            }
            else if (BtnSubmit == "添加")
            {
                userBusinessLayer.InsertUser(user);
            }
                
            return RedirectToAction("Information");

            //CreateUserViewModel v = new CreateUserViewModel();
            //v.user = userBusinessLayer.GetUser(Session["User"].ToString());
            //return PartialView("CreateUser", v);
        }

        [HttpPost]
        public ActionResult AddUser(User user, string BtnSubmit)
        {
            //CreateUserViewModel v = new CreateUserViewModel();
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if (BtnSubmit == "添加")
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
        #endregion

        #region 物品界面
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
        public ActionResult SaveCommodity(Commodity model, string BtnSubmit)
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
                commodityBusinessLayer.UpdataCommoditys(model.Co_Id.ToString(), model);
                return RedirectToAction("Attributes");
            }

            //如果不是按键操作，刷新本页面
            return PartialView("CreateCommodity"); 
        }
        #endregion

        #region 存储界面

        #region 存储界面显示
        [HttpGet]
        public ActionResult Storage()
        {
            //StorageViewModel storageViewModel = new StorageViewModel();
            //storageViewModel.existTableListViewModel = new ExistTableListViewModel();
            //storageViewModel.existTableListViewModel.existTableViewModels = new List<ExistTableViewModel>();
            //storageViewModel.UserName = Session["User"].ToString();
            IsSearchExist = false;
            MyStorageBusinessLayer storageBusinessLayer = new MyStorageBusinessLayer();
            //判断是否为管理员，是管理员则为空，不是则为none，对应修改按钮是否显示
            bool Display = (level.Admin == (level)Session["level"]);
            //获取显示的数据
            StorageViewModel storageViewMode1 = storageBusinessLayer.GetStorageViewModel(Display, Session["User"].ToString());

            return View("Storage", storageViewMode1);
        }
        #endregion

        #region 重定向专用 
        [HttpGet]
        public ActionResult RedirectStorage()
        {
            MyStorageBusinessLayer storageBusinessLayer = new MyStorageBusinessLayer();
            //判断是否为管理员，是管理员则为空，不是则为none，对应修改按钮是否显示
            bool Display = (level.Admin == (level)Session["level"]);
            //获取显示的数据
            StorageViewModel storageViewMode1;
            while(IsSearchExist)
            {
                //获取user id
                string U_id = null;
                if(!S_userMember.Equals(new UserMember()))
                    if (S_userMember.U_Id != "" || S_userMember.U_name != "")
                    {
                        UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                        List<User> users = userBusinessLayer.GetUsers(S_userMember);
                        if (users == null)
                            break;
                        U_id = users.FirstOrDefault().U_Id.ToString();
                    }

                //获取全部exist的数据
                storageViewMode1 = storageBusinessLayer.GetStorageViewModel(Display, Session["User"].ToString());

                //获取comm id集合
                List<Commodity> commodities = new List<Commodity>();
                if (!S_commodityMember.Equals(new CommodityMember()))
                {
                    CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
                    commodities = commodityBusinessLayer.GetCommodities(S_commodityMember);
                }

                //获取ware id集合
                List<Warehouse> warehouses = new List<Warehouse>();
                if (!S_warehouseMember.Equals(new WarehouseMember()))
                {
                    WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
                    warehouses = warehouseBusinessLayer.GetWarehouse(S_warehouseMember);
                }

                bool[] k = new bool[3];
                ExistTableViewModel existTableViewModel;
                for(int i = storageViewMode1.existTableListViewModel.existTableViewModels.Count() - 1; i >= 0; i--) 
                {
                    k[0] = k[1] = k[2] = true;
                    existTableViewModel = storageViewMode1.existTableListViewModel.existTableViewModels[i];
                    if (U_id == null || existTableViewModel.exist.U_id.ToString() == U_id)
                        k[0] = false;
                    if (commodities != null && commodities.Count() > 0)
                        foreach (Commodity commodity in commodities)
                        {
                            if (commodity.Co_Id == existTableViewModel.exist.Co_id)
                            {
                                k[1] = false;
                                break;
                            }
                        }
                    else
                        k[1] = false;
                    if (warehouses != null && warehouses.Count() > 0)
                        foreach (Warehouse warehouse in warehouses)
                        {
                            if (warehouse.Wa_Id == existTableViewModel.exist.W_id)
                            {
                                k[2] = false;
                                break;
                            }
                        }
                    else
                        k[2] = false;
                    if(k[0] || k[1] || k[2])
                    {
                        storageViewMode1.existTableListViewModel.existTableViewModels.Remove(existTableViewModel);
                    }
                }

               // storageViewMode1 = storageBusinessLayer.GetStorageViewModel(Display, Session["User"].ToString());
                return View("Storage", storageViewMode1);
            }

            storageViewMode1 = storageBusinessLayer.GetStorageViewModel(Display, Session["User"].ToString());
            return View("Storage", storageViewMode1);
        }

        #endregion

        #region 单项搜索
        [HttpPost]
        public ActionResult searchStorage(string Select, string uname, string BtnSubmit)
        {
            MyStorageBusinessLayer storageBusinessLayer = new MyStorageBusinessLayer();
            //判断是否为管理员，是管理员则为空，不是则为none，对应修改按钮是否显示
            bool Display = (level.Admin == (level)Session["level"]);
            //获取显示的数据
            StorageViewModel storageViewMode1 = storageBusinessLayer.GetStorageViewModel(Display, Session["User"].ToString(), Select, uname);

            return View("Storage", storageViewMode1);
        }
        #endregion

        #region 多项搜索
        [HttpPost]
        public ActionResult moreSearchStorage(SearchStorageViewModel searchStorageViewModel, string BtnSubmit)
        {
            IsSearchExist = true;
            S_userMember.Clear();
            S_commodityMember.Clear();
            S_existMember.Clear();
            S_warehouseMember.Clear();
            //改变全局变量的值，使重定向显示搜索到的表
            S_userMember.U_Id = searchStorageViewModel.U_Id;
            S_userMember.U_name = searchStorageViewModel.U_name;

            S_commodityMember.Co_Id = searchStorageViewModel.Co_Id;
            S_commodityMember.Co_name = searchStorageViewModel.Co_name;
            S_commodityMember.Co_bar_code = searchStorageViewModel.Co_bar_code;
            S_commodityMember.Co_price = searchStorageViewModel.Co_price;
            S_commodityMember.Co_specification = searchStorageViewModel.Co_specification;
            S_commodityMember.Co_type = searchStorageViewModel.Co_type;
            S_commodityMember.Co_unit = searchStorageViewModel.Co_unit;
            S_commodityMember.Co_weight = searchStorageViewModel.Co_weight;

            S_warehouseMember.Wa_name = searchStorageViewModel.W_name;
            S_warehouseMember.Wa_Id = searchStorageViewModel.Wa_Id;

            S_existMember.Star_date = searchStorageViewModel.start_date.ToString();
            S_existMember.End_date =  searchStorageViewModel.end_date.ToString();
            //重定向
            return RedirectToAction("RedirectStorage");
        }
        #endregion

        #region 报损处理
        [HttpPost]
        public ActionResult LossExist(string Count, string Co_Id, string IO_Id)
        {
            //MyStorageBusinessLayer storageBusinessLayer = new MyStorageBusinessLayer();
            //减去要报损的数据
            //通过IO_Id和Co_Id查询到符合条件的Exist
            ExistBusinessLayer existBusinessLayer = new ExistBusinessLayer();
            if(Convert.ToInt32(Count) > 0)
                existBusinessLayer.InputExist(IO_Id, Co_Id, Convert.ToInt32(Count));            

            return RedirectToAction("RedirectStorage");
        }
        #endregion

        #region 修改信息
        public ActionResult SaveExist(Exist exist, string W_name, string U_name)
        {
            //通过key查询到要改变的exit
            //通过W_name和U_name分别获得仓库编号和用户编号
            //整合信息
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            if((exist.U_id = userBusinessLayer.GetId(U_name)) == -1)
                return RedirectToAction("RedirectStorage");
            WarehouseBusinessLayer warehouseBusinessLayer = new WarehouseBusinessLayer();
            if ((exist.W_id = warehouseBusinessLayer.GetId(W_name)) == -1)
                return RedirectToAction("RedirectStorage");
            //修改信息
            ExistBusinessLayer existBusinessLayer = new ExistBusinessLayer();
            existBusinessLayer.InputExist(exist.IO_Id, exist);

            //重定向
            return RedirectToAction("RedirectStorage");
        }
        #endregion

        #endregion

        #region 仓库界面
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
            return PartialView("CreateWarehouse", createWarehouseViewModel); 
        }

        /// <summary>
        /// 删除表称谓为T_name中id为ViewBag["D_id"]的数据
        /// </summary>
        /// <param name="T_name">要操作的表</param>
        /// <param name="D_name">要删除的名称</param>
        /// <param name="BtnSubmit">按下的按键名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteItem(string T_name, string D_name, string D_id, string BtnSubmit)
        {
            if (BtnSubmit == "确定")
            {
                switch(T_name)
                {
                    case "物品":
                        //Commodity表的删除操作
                        CommodityBusinessLayer commodityBusinessLayer = new CommodityBusinessLayer();
                        commodityBusinessLayer.DeleteCommodity(D_id);
                        return RedirectToAction("Attributes");
                    case "用户":
                        UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                        userBusinessLayer.DeleteUser(D_id);
                        return RedirectToAction("Information");
                    default:
                        return RedirectToAction("Warehouse");
                }
            }
            else if (BtnSubmit == "取消")
            {
                return RedirectToAction("Warehouse");
            }

            //如果不是按键操作，刷新本页面
            CreateWarehouseViewModel createWarehouseViewModel = new CreateWarehouseViewModel();
            createWarehouseViewModel.warehouse = new Warehouse();
            return PartialView("CreateWarehouse", createWarehouseViewModel); 
        }
        #endregion
    }
}