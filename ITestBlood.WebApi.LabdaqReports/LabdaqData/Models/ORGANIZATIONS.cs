namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ORGANIZATIONS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal ORG_ID { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

        [StringLength(50)]
        public string ORG_NAME { get; set; }

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

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(4000)]
        public string EMAIL_ADDRESS { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        [StringLength(9)]
        public string FW_PREFIX { get; set; }

        [StringLength(1)]
        public string PRINT_DEFAULT { get; set; }

        [StringLength(1)]
        public string FAX_DEFAULT { get; set; }

        [StringLength(1)]
        public string EMAIL_DEFAULT { get; set; }

        [StringLength(255)]
        public string DEFAULT_PRINTER { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RPT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORG_TYPE_ID { get; set; }

        public virtual ORGANIZATIONS_DETAILS ORGANIZATIONS_DETAILS { get; set; }
    }
}
