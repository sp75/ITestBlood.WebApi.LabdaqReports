using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class OrderResultData
    {
        public DateTime CreatedDate { get; set; }
        public string PanelName { get; set; }
        public string AlphaRangeText { get; set; }
        public string PanelId { get; set; }
        public string TestName { get; set; }
        public string TestId { get; set; }
        public int RpId { get; set; }
        public string PrintNotes { get; set; }
        public int? DecPlaces { get; set; }
        public string PatId { get; set; }
        public string PanelNotes { get; set; }
        public bool TestRequired { get; set; }
        public DateTime? RunDate { get; set; }
        public string PanelType { get; set; }
        public string PanelStatus { get; set; }
        public int AddedAfterPrint { get; set; }
        public string TestResultType { get; set; }
        public int IsAllergen { get; set; }
        public string StorageStability { get; set; }
    }
}
