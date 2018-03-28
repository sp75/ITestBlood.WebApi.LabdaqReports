namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RL_REQ_PANEL_CPTS
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal RP_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string CPT_ID { get; set; }

        [StringLength(2)]
        public string MOD1 { get; set; }

        [StringLength(2)]
        public string MOD2 { get; set; }

        [StringLength(2)]
        public string MOD3 { get; set; }
    }
}
