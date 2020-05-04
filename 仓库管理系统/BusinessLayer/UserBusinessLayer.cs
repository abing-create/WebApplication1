using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓库管理系统.DataAccessLayer;
using 仓库管理系统.Models;

namespace 仓库管理系统.BusinessLayer
{
    public class UserBusinessLayer
    {
       /// <summary>
       /// 查询数据库TblUser表
       /// </summary>
       /// <returns></returns>
        public List<User> GetUser()
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            return dB.users.ToList();
        }

        /// <summary>
        /// 插入数据到表TblUser
        /// </summary>
        /// <param name="user">插入的数据</param>
        /// <returns></returns>
        public User InsertUser(User user)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.users.Add(user);

            //while (true)
            //{
            //    salesDal.SaveChanges();
            //    var model = salesDal.Employees.Where(c => c.LastName == "diao").FirstOrDefault();
            //    if (model == null)
            //        break;
            //     model.LastName = "cao";
            //}
            

            dB.SaveChanges();
            return user;
        }

        /// <summary>
        /// 修改数据到表TblUser
        /// </summary>
        /// <param name="name">User的U_nam名字</param>
        /// <param name="user">更新成的表</param>
        public void UpdataUsers(string name, User user)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.users.Where(c => c.U_name == "cao").FirstOrDefault();
            if (model != null)
            {
                model = user;
            //    foreach (var m in model)
            //    {
            //        m.U_name = "diao";
            //    }
            }
        }

        /// <summary>
        /// 插入多个数据到表TblUser
        /// </summary>
        /// <param name="users"></param>
        public void InsertUsers(List<User> users)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.users.AddRange(users);
            dB.SaveChanges();
        }
    }
}
