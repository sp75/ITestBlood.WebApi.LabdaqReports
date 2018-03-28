namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_Doctors
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal DOC_ID { get; set; }

        [StringLength(25)]
        public string L_NAME { get; set; }

        [StringLength(20)]
        public string M_NAME { get; set; }

        [StringLength(20)]
        public string F_NAME { get; set; }

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

        [StringLength(30)]
        public string FAX_PHONE { get; set; }

        [StringLength(15)]
        public string BEEPER_PHONE { get; set; }

        [StringLength(15)]
        public string CELL_PHONE { get; set; }

        [StringLength(20)]
        public string UPIN_NO { get; set; }

        [StringLength(10)]
        public string NPI { get; set; }

        [StringLength(20)]
        public string MED_ASSIST_NO { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(4000)]
        public string EMAIL_ADDRESS { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DISTRICT_ID { get; set; }

        [StringLength(50)]
        public string REGION_NAME { get; set; }

        [StringLength(50)]
        public string ORG_NAME { get; set; }

        [StringLength(50)]
        public string CLIENT_REP { get; set; }

        [StringLength(10)]
        public string EMR { get; set; }

        [StringLength(50)]
        public string SERVICE_REP { get; set; }

        [StringLength(50)]
        public string ORG_ADDRESS1 { get; set; }

        [StringLength(50)]
        public string ORG_ADDRESS2 { get; set; }

        [StringLength(25)]
        public string ORG_CITY { get; set; }

        [StringLength(2)]
        public string ORG_STATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORG_ID { get; set; }

        [StringLength(10)]
        public string ORG_ZIP { get; set; }

        [StringLength(15)]
        public string ORG_PHONE { get; set; }

        [StringLength(4000)]
        public string ORG_EMAIL_ADDRESS { get; set; }

        public Guid? ID { get; set; }

        [StringLength(30)]
        public string ORG_FAX_PHONE { get; set; }

        [StringLength(6)]
        public string ORG_PHONE_EXT { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }
    }
}
