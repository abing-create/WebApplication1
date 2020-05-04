using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 仓储系统.ViewModels
{
    public class LoginViewModel
    {
        [StringLength(7, MinimumLength = 2, ErrorMessage = "UserName length should be between 2 and 7")]
        public string U_name { get; set; }
        public string U_password { get; set; }
        public char U_level { get; set; }
    }
}