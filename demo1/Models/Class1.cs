﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo1.Models
{
    public class Class1
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
    }
}