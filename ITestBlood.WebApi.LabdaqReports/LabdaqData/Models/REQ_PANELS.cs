namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REQ_PANELS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal RP_ID { get; set; }

        [StringLength(1)]
        public string DEL_FLAG { get; set; }

        [StringLength(1)]
        public string APPROVAL_REQUIRED { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ACC_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SG_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PANEL_ID { get; set; }

        [StringLength(1)]
        public string DX1 { get; set; }

        [StringLength(1)]
        public string DX2 { get; set; }

        [StringLength(1)]
        public string DX3 { get; set; }

        [StringLength(1)]
        public string DX4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOC_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RUN_DATE { get; set; }

        [StringLength(3)]
        public string RUN_BY { get; set; }

        [StringLength(1)]
        public string UPLOADED { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CREATED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CREATED_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DELETED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DELETED_BY { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? APPROVED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? APPROVED_BY { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(1)]
        public string FAIL_TYPE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LD_REFLEX_RP_ID { get; set; }

        [StringLength(1)]
        public string STAT { get; set; }

        [StringLength(4000)]
        public string DELETE_REASON_NOTE { get; set; }

        [StringLength(1)]
        public string DX5 { get; set; }

        [StringLength(1)]
        public string DX6 { get; set; }

        [StringLength(1)]
        public string DX7 { get; set; }

        [StringLength(1)]
        public string DX8 { get; set; }

        [StringLength(3)]
        public string ACCEPTED_BY { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ACCEPTED_BY_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RUN_BY_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UNIT_COUNT { get; set; }

        [StringLength(30)]
        public string APPROVAL_REASON { get; set; }

        [StringLength(4000)]
        public string INTERNAL_NOTES { get; set; }
    }
}
