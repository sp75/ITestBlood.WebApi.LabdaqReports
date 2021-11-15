using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class PanelResultData
    {
        public Int32 RpId { get; set; }
        public int AccId { get; set; }
        public string PanelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string TestId { get; set; }
        public decimal? ResultNumeric { get; set; }
        public string ResultAlpha { get; set; }
        public int Corrected { get; set; }
        public int DisplayOnReport { get; set; }
        public decimal? LowOrMean { get; set; }
        public decimal? HighOrSd { get; set; }
        public string Units { get; set; }
        public string Flag { get; set; }
        public string ResultCodeText { get; set; }
        public string ResultTranslation { get; set; }
        public int IsPositive { get; set; }
        public DateTime? PrintedDate { get; set; }
        public string PanelName { get; set; }
        public string CorrectedBy { get; set; }
    }

}
