using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace 弹出.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }


        [StringLength(5, ErrorMessage = "Last Name length should not be greater than 5")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}