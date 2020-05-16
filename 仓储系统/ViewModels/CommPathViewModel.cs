using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 仓储系统.Models;

namespace 仓储系统.ViewModels
{
    public class CommPathViewModel
    {
        public string path { get; set; }

        public Commodity commodity { get; set; }
    }
}