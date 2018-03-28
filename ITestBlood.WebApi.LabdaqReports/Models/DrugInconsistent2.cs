using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
   public class DrugInconsistent2
    {
        public string TestId { get; set; }
        public string DetectedAnalyte { get; set; }
        public string MeasuredResultNumeric { get; set; }
        public string Cutoff { get; set; }
        public string DetectionWindow { get; set; }
        public string Outcome { get; set; }
        public string Units { get; set; }
    }
}
