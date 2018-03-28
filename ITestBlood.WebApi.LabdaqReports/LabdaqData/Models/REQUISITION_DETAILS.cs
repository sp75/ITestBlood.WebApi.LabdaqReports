namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REQUISITION_DETAILS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal AccId { get; set; }

        public string SpecimenOrderNote { get; set; }

        [StringLength(250)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual REQUISITIONS REQUISITIONS { get; set; }
    }
}
