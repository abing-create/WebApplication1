using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.Models
{

    public struct UserMember
    {
        public string U_Id;
        public string U_name;
        public string U_password;
        public string U_post;
        public string U_department;
        public string U_birthday;
        public string U_phone;
        public string U_level;
        public string U_point;
        public string U_sex;

        public void Clear()
        {
            U_Id = null;
            U_name = null;
            U_password = null;
            U_post = null;
            U_department = null;
            U_birthday = null;
            U_phone = null;
            U_level = null;
            U_point = null;
            U_sex = null;
        }
    }

    public enum level
    {
        Admin,//管理员
        Staff,//普通员工
        Director//仓库看管
    }
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int U_Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [FirstNameValidation]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "用户名必须大于5位数小于20位数")]
        public string U_name { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码必须大于6位数小于20位数")]
        public string U_password { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [StringLength(50)]
        public string U_post { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [StringLength(50)]
        public string U_department { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime U_birthday { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(50)]
        public string U_phone { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public level U_level { get; set; }

        /// <summary>
        /// 密码提示
        /// </summary>
        [StringLength(100)]
        public string U_point { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(2)]
        public string U_sex { get; set; }
    }

    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) // Checking for Empty Value
            {
                return new ValidationResult("请填入名称");
            }
            else
            {
                if (value.ToString().Contains("@"))
                {
                    return new ValidationResult("名称中不能含有@符号");
                }
            }
            return ValidationResult.Success;
        }
    }
}