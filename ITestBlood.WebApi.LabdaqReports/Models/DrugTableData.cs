using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Models
{
    public class DrugTableData
    {
        public List<DrugConsistent> Consistent { get; set; }
        public List<DrugInconsistent1> Inconsistent1 { get; set; }
        public List<DrugInconsistent2> Inconsistent2 { get; set; }
    }
}
