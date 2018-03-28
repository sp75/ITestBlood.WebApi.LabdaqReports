namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INS_COMPANIES_DETAILS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal InsId { get; set; }

        public int Adjustment { get; set; }

        public virtual INS_COMPANIES INS_COMPANIES { get; set; }
    }
}
