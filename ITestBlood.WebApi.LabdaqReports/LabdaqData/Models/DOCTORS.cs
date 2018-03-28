namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DOCTORS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal DOC_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORG_ID { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

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

        [StringLength(100)]
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

        [StringLength(20)]
        public string UPIN_NO { get; set; }

        [StringLength(15)]
        public string H_PHONE { get; set; }

        [StringLength(15)]
        public string W_PHONE { get; set; }

        [StringLength(6)]
        public string W_EXT { get; set; }

        [StringLength(30)]
        public string FAX_PHONE { get; set; }

        [StringLength(15)]
        public string BEEPER_PHONE { get; set; }

        [StringLength(15)]
        public string CELL_PHONE { get; set; }

        [StringLength(4000)]
        public string EMAIL_ADDRESS { get; set; }

        [StringLength(25)]
        public string COMPANY_NAME { get; set; }

        [StringLength(1)]
        public string PRINT_DEFAULT { get; set; }

        [StringLength(1)]
        public string FAX_DEFAULT { get; set; }

        [StringLength(255)]
        public string DEFAULT_PRINTER { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RPT_ID { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [StringLength(4000)]
        public string PANIC_COMMENTS { get; set; }

        [StringLength(20)]
        public string LICENSE_NO { get; set; }

        [StringLength(20)]
        public string MED_ASSIST_NO { get; set; }

        [StringLength(255)]
        public string SETTINGS_MASK { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        [StringLength(1)]
        public string EMAIL_DEFAULT { get; set; }

        [StringLength(4000)]
        public string EMAIL_BODY { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DISTRICT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? REGION_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DEFAULT_REQ_LOC_ID { get; set; }

        [StringLength(10)]
        public string NPI { get; set; }
    }
}
