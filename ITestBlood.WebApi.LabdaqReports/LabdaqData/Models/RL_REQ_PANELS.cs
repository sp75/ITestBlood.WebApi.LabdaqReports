namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RL_REQ_PANELS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal RP_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ACC_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RL_ID { get; set; }

        [StringLength(1)]
        public string DEL_FLAG { get; set; }

        [StringLength(25)]
        public string PROFILE_ID { get; set; }

        [StringLength(75)]
        public string PROFILE_NAME { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RUN_DATE { get; set; }

        [StringLength(1)]
        public string FAIL_TYPE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UPLOAD_DATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CREATED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CREATED_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DELETED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DELETED_BY { get; set; }

        [StringLength(1)]
        public string DX1 { get; set; }

        [StringLength(1)]
        public string DX2 { get; set; }

        [StringLength(1)]
        public string DX3 { get; set; }

        [StringLength(1)]
        public string DX4 { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [StringLength(4000)]
        public string IMPORT_NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REFLEX_RP_ID { get; set; }

        [StringLength(15)]
        public string PERF_SITE_ID { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? SG_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UNIT_COUNT { get; set; }

        [StringLength(15)]
        public string LOINC_CODE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RECEIVED_DATE { get; set; }

        [StringLength(4000)]
        public string INTERNAL_NOTES { get; set; }
    }
}
