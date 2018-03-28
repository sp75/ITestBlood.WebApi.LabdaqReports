using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class EventData
    {
        public string TestId { get; set; }
        public decimal? LessThan { get; set; }
        public decimal? GreaterThan { get; set; }
        public string ResultTranslation { get; set; }
        public string AlphaText { get; set; }
        public string Flag { get; set; }
        public bool IsFlag { get; set; }
    }
}
