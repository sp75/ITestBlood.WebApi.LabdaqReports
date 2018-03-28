using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class OrderInfoView
    {
        public int AccId { get; set; }
        public string FinalStatus { get; set; }
        public DateTime? PrintedDate { get; set; }
        public string Notes { get; set; }
        public string PatId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
