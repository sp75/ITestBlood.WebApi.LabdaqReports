namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REQUISITIONS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal ACC_ID { get; set; }

        [StringLength(1)]
        public string DEL_FLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_ID1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_OPT1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_ID2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_OPT2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_ID3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_OPT3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_ID4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_OPT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOC_ID { get; set; }

        [StringLength(30)]
        public string PAT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PI_ID1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PI_ID2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PI_ID3 { get; set; }

        [StringLength(10)]
        public string FASTING { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CREATED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CREATED_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DELETED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DELETED_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? REQ_DATE { get; set; }

        [StringLength(3)]
        public string REQ_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DRAW_DATE { get; set; }

        [StringLength(3)]
        public string DRAW_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RECEIVED_DATE { get; set; }

        [StringLength(3)]
        public string RECEIVED_BY { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PRINTED_DATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RL_PRINTED_DATE { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE1 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE2 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE3 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE4 { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WS_ID { get; set; }

        [StringLength(30)]
        public string REQ_TYPE { get; set; }

        [StringLength(1)]
        public string LABEL_PRINTED { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PV_ID { get; set; }

        [StringLength(1)]
        public string REVIEW { get; set; }

        [StringLength(4000)]
        public string MICRO_VISIBLE_GROUPS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORG_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DISTRICT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REGION_ID { get; set; }

        [StringLength(4000)]
        public string DELETE_REASON_NOTE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SPLIT_ACC_ID { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE5 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE6 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE7 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE8 { get; set; }

        [StringLength(4000)]
        public string INTERNAL_NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REGION_OPT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DISTRICT_OPT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORG_OPT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOC_OPT { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE9 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE10 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE11 { get; set; }

        [StringLength(10)]
        public string DIAGNOSIS_CODE12 { get; set; }

        public virtual REQUISITION_DETAILS REQUISITION_DETAILS { get; set; }
    }
}
