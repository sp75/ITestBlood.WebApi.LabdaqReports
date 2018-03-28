namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_INS_COMPANIES
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal INS_ID { get; set; }

        [StringLength(50)]
        public string INS_NAME { get; set; }

        public int? Adjustment { get; set; }
    }
}
