using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebMVC.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<double> TotalMoney { get; set; }
    }
}