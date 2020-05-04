using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.DataAccessLayer
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

        public User GetUser(string name)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var user = dB.users.Where(c => c.U_name == name).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return (new User());
        }

        /// <summary>
        /// 插入数据到表TblUser
        /// </summary>
        /// <param name="user">插入的数据</param>
        /// <returns></returns>
        public User InsertUser(User user)
        {
            //dB.users.Add(user);
            //dB.SaveChanges();
            //return user;

            WarehouseERPDAL dB = new WarehouseERPDAL();
            dB.users.Add(user);
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
            var model = dB.users.Where(c => c.U_name == name).FirstOrDefault();
            if (model != null)
            {
                //model.U_password = user.U_password;
                Change(ref model, user);
            }
            dB.SaveChanges();
        }

        private void Change(ref User model, User user)
        {
            model.U_birthday = user.U_birthday;
            model.U_department = user.U_department;
            model.U_Id = user.U_Id;
            model.U_level = user.U_level;
            model.U_name = user.U_name;
            model.U_password = user.U_password;
            model.U_phone = user.U_phone;
            model.U_point = user.U_point;
            model.U_post = user.U_post;
            model.U_sex = user.U_sex;
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