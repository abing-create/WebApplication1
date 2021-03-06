﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.DataAccessLayer;
using 仓储系统.Models;

namespace 仓储系统.BusinessLayer
{
    public class LoginBusinessLayer
    {
        private UserBusinessLayer userBusinessLayer { get; set; }

        public LoginBusinessLayer()
        {
            userBusinessLayer = new UserBusinessLayer();
        }

        /// <summary>
        /// 是否可以登录成功
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool IsLogin(string name, string password)
        {
            //UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            List<User> users = userBusinessLayer.GetUser();
            User user = users.Where(c => c.U_name == name).FirstOrDefault();
            //User user = userBusinessLayer.GetUser(name);
            if (user == null || user.Equals(new User()))
            {
                return false;
            }
            else if(user.U_password != password)
            {
                user.U_fail++;
                userBusinessLayer.UpdataFail(user);
                return false;
            }
            user.U_fail = 0;
            saveRecord(user.U_Id, name);
            return true;            
        }

        public bool IsFail(string name)
        {
            List<User> users = userBusinessLayer.GetUser();
            User user = users.Where(c => c.U_name == name).FirstOrDefault();
            if (!user.Equals(new User()) && user.U_fail > 5)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 存储登录信息
        /// </summary>
        /// <param name="id">登录人id</param>
        /// <param name="name">登录人姓名</param>
        public void saveRecord(int id, string name)
        {
            Record record = new Record();
            record.U_id = id;
            record.U_name = name;
            record.Record_date = DateTime.Now;

            RecordBusinessLayer recordBusinessLayer = new RecordBusinessLayer();
            recordBusinessLayer.InsertRecord(record);
        }

        public level getLevel(string name)
        {
            User user = userBusinessLayer.GetUser(name);
            level mylevel = user.U_level;
            return mylevel;
        }
    }
}