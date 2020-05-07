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

        /// <summary>
        /// 获取符合select属性为uname的用户
        /// </summary>
        /// <param name="select"></param>
        /// <param name="uname"></param>
        /// <returns></returns>
        public List<User> GetUsers(string select, string uname)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<User> users;
            switch(select)
            {
                case "员工编号":
                    users = dB.users.Where(c => c.U_Id.ToString() == uname).ToList();
                    break;
                case "员工等级":
                    users = dB.users.Where(c => c.U_level.ToString() == uname).ToList();
                    break;
                case "员工职务":
                    users = dB.users.Where(c => c.U_post == uname).ToList();
                    break;
                case "员工部门":
                    users = dB.users.Where(c => c.U_department == uname).ToList();
                    break;
                case "员工姓名":
                    users = dB.users.Where(c => c.U_name == uname).ToList();
                    break;
                case "联系电话":
                    users = dB.users.Where(c => c.U_phone == uname).ToList();
                    break;
                case "员工性别":
                    users = dB.users.Where(c => c.U_sex == uname).ToList();
                    break;
                default:
                    users = new List<User>();
                    break;
            }
            return users;
        }

        /// <summary>
        /// 符合条件集合的
        /// </summary>
        /// <param name="userMember"></param>
        /// <returns></returns>
        public List<User> GetUsers(UserMember userMember)
        {
            WarehouseERPDAL dB = new WarehouseERPDAL();
            List<User> users;
            users = dB.users.Where(c => (userMember.U_birthday == null || c.U_birthday.ToString() == userMember.U_birthday) &&
            (userMember.U_department == null || c.U_department == userMember.U_department) &&
            (userMember.U_Id == null || c.U_Id.ToString() == userMember.U_Id) &&
            (userMember.U_level == null || c.U_level.ToString() == userMember.U_level) &&
            (userMember.U_name == null || c.U_name == userMember.U_name) &&
            (userMember.U_password == null || c.U_password == userMember.U_password) &&
            (userMember.U_phone == null || c.U_department == userMember.U_phone) &&
            (userMember.U_point == null || c.U_department == userMember.U_point) &&
            (userMember.U_post == null || c.U_department == userMember.U_post) &&
            (userMember.U_sex == null || c.U_sex == userMember.U_sex)).ToList();

            return users == null ? new List<User>() : users;
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

        public void DeleteUser(string id)
        {
            //int my_id = Convert.ToInt32(id);
            WarehouseERPDAL dB = new WarehouseERPDAL();
            var model = dB.users.Where(c => c.U_Id.ToString() == id).FirstOrDefault();
            if (model != null)
                dB.users.Remove(model);
            dB.SaveChanges();
        }
    }
}