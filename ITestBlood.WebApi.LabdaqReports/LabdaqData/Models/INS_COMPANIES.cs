namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INS_COMPANIES
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal INS_ID { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

        [StringLength(50)]
        public string INS_NAME { get; set; }

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

        [StringLength(50)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(1)]
        public string PLAN_TYPE { get; set; }

        [StringLength(15)]
        public string PHONE { get; set; }

        [StringLength(6)]
        public string PHONE_EXT { get; set; }

        [StringLength(40)]
        public string CONTACT1 { get; set; }

        [StringLength(40)]
        public string CONTACT2 { get; set; }

        [StringLength(15)]
        public string CONTACT1_PHONE { get; set; }

        [StringLength(15)]
        public string CONTACT2_PHONE { get; set; }

        [StringLength(6)]
        public string CONTACT1_PHONE_EXT { get; set; }

        [StringLength(6)]
        public string CONTACT2_PHONE_EXT { get; set; }

        [StringLength(30)]
        public string FAX_PHONE { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [StringLength(200)]
        public string ALERT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? IG_ID { get; set; }

        [StringLength(1)]
        public string MEDICAL_NECESSITY { get; set; }

        public virtual INS_COMPANIES_DETAILS INS_COMPANIES_DETAILS { get; set; }
    }
}
