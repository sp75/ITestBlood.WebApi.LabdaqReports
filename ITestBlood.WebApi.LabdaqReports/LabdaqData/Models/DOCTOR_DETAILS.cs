namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DOCTOR_DETAILS
    {
        public Guid ID { get; set; }

        [Key]
        [Column(TypeName = "numeric")]
        public decimal DOC_ID { get; set; }
    }
}
