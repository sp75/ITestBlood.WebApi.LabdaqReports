namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PATIENTS
    {
        [Key]
        [StringLength(30)]
        public string PAT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PAT_ID_NUMERIC { get; set; }

        [StringLength(20)]
        public string CONTROL_ID2 { get; set; }

        [StringLength(20)]
        public string CONTROL_ID3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CONTROL_LEVEL { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REIMBURSE_ID { get; set; }

        [StringLength(1)]
        public string GT_RELATIONSHIP { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DOC_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SPECIES_ID { get; set; }

        [StringLength(25)]
        public string L_NAME { get; set; }

        [StringLength(20)]
        public string F_NAME { get; set; }

        [StringLength(20)]
        public string M_NAME { get; set; }

        [StringLength(10)]
        public string PREFIX { get; set; }

        [StringLength(10)]
        public string SUFFIX { get; set; }

        [StringLength(50)]
        public string ADDRESS1 { get; set; }

        [StringLength(50)]
        public string ADDRESS2 { get; set; }

        [StringLength(25)]
        public string CITY { get; set; }

        [StringLength(2)]
        public string STATE { get; set; }

        [StringLength(10)]
        public string ZIP { get; set; }

        [StringLength(11)]
        public string SSN { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BIRTH { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DEATH { get; set; }

        [StringLength(1)]
        public string GENDER { get; set; }

        [StringLength(1)]
        public string RACE { get; set; }

        [StringLength(20)]
        public string MED_REC_NO { get; set; }

        [StringLength(15)]
        public string H_PHONE { get; set; }

        [StringLength(15)]
        public string W_PHONE { get; set; }

        [StringLength(6)]
        public string W_EXT { get; set; }

        [StringLength(50)]
        public string EMAIL_ADDRESS { get; set; }

        [StringLength(200)]
        public string ALERT { get; set; }

        [StringLength(1)]
        public string MARITAL_STATUS { get; set; }

        [StringLength(30)]
        public string DRIVER_NO { get; set; }

        [StringLength(50)]
        public string EMPLOYER { get; set; }

        [StringLength(30)]
        public string OCCUPATION { get; set; }

        [StringLength(50)]
        public string EMP_ADDRESS1 { get; set; }

        [StringLength(50)]
        public string EMP_ADDRESS2 { get; set; }

        [StringLength(25)]
        public string EMP_CITY { get; set; }

        [StringLength(2)]
        public string EMP_STATE { get; set; }

        [StringLength(10)]
        public string EMP_ZIP { get; set; }

        [StringLength(1)]
        public string EMP_STATUS { get; set; }

        [StringLength(1)]
        public string SCHOOL_STATUS { get; set; }

        [StringLength(50)]
        public string EC_NAME { get; set; }

        [StringLength(25)]
        public string EC_RELATIONSHIP { get; set; }

        [StringLength(50)]
        public string EC_ADDRESS1 { get; set; }

        [StringLength(50)]
        public string EC_ADDRESS2 { get; set; }

        [StringLength(25)]
        public string EC_CITY { get; set; }

        [StringLength(2)]
        public string EC_STATE { get; set; }

        [StringLength(10)]
        public string EC_ZIP { get; set; }

        [StringLength(15)]
        public string EC_H_PHONE { get; set; }

        [StringLength(15)]
        public string EC_W_PHONE { get; set; }

        [StringLength(6)]
        public string EC_W_EXT { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [StringLength(10)]
        public string WG_FLAGS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ANALYZER_ID { get; set; }

        [StringLength(200)]
        public string REMINDER { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ABN_RPT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORG_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DISTRICT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REGION_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CONTROL_LOC_ID { get; set; }

        [StringLength(1)]
        public string PAT_TYPE { get; set; }

        [StringLength(1)]
        public string CTRL_RESTRICT_LIMIT { get; set; }

        [StringLength(1)]
        public string CTRL_RESTRICT_WARNING { get; set; }

        [StringLength(1)]
        public string CTRL_RESTRICT_REJECT { get; set; }

        [StringLength(1)]
        public string CTRL_INCLUDE_DELETED { get; set; }
    }
}
