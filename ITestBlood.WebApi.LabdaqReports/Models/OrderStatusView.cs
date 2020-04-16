using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class OrderStatusView
    {
        public int AccId { get; set; }
        public string FinalStatus { get; set; }
        public bool HasAbnormalTests { get; set; }
    }
}