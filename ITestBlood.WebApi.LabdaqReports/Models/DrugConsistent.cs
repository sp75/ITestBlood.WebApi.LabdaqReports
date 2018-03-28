using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class DrugConsistent
    {
        public string TestId { get; set; }
        public string ReportedPrescription { get; set; }
        public string AnticipatedPositives { get; set; }
        public string Outcome { get; set; }
        public string DetectionWindow { get; set; }
        public string Units { get; set; }
    }
}
