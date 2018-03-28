namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USER_INFO
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal USER_ID { get; set; }

        [StringLength(10)]
        public string USER_NAME { get; set; }

        [StringLength(3)]
        public string USER_INITIALS { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PASSWORD_MODIFIED { get; set; }

        [StringLength(20)]
        public string F_NAME { get; set; }

        [StringLength(20)]
        public string M_NAME { get; set; }

        [StringLength(25)]
        public string L_NAME { get; set; }

        [StringLength(35)]
        public string TITLE { get; set; }

        [StringLength(11)]
        public string SSN { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BIRTH { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? HIRED { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TERMINATED { get; set; }

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

        [StringLength(15)]
        public string H_PHONE { get; set; }

        [StringLength(15)]
        public string W_PHONE { get; set; }

        [StringLength(6)]
        public string W_EXT { get; set; }

        [StringLength(15)]
        public string BEEPER_PHONE { get; set; }

        [StringLength(15)]
        public string CELL_PHONE { get; set; }

        [StringLength(50)]
        public string EMAIL_ADDRESS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WINDOW_COUNT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOG_OFF_MINS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PASSWORD_EXPIRE_DAYS { get; set; }

        [StringLength(1)]
        public string APPROVAL_REQUIRED { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LOC_ID { get; set; }

        [StringLength(1)]
        public string CHOOSE_LOCATION { get; set; }

        [StringLength(1)]
        public string IS_FIREWALLED { get; set; }

        [StringLength(1)]
        public string ALLOW_WEB_ACCESS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SIGNATURE_ATTACH_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? APPROVAL_PANEL_SET_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PRINTER_MAPPING_ID { get; set; }
    }
}
