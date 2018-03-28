namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ORGANIZATIONS_DETAILS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal ORG_ID { get; set; }

        [StringLength(500)]
        public string NOTE { get; set; }

        public virtual ORGANIZATIONS ORGANIZATIONS { get; set; }
    }
}
