namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PAT_INSURANCE
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal PI_ID { get; set; }

        [StringLength(30)]
        public string PAT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? INS_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PRIORITY { get; set; }

        [StringLength(25)]
        public string L_NAME { get; set; }

        [StringLength(20)]
        public string F_NAME { get; set; }

        [StringLength(20)]
        public string M_NAME { get; set; }

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

        [Column(TypeName = "datetime2")]
        public DateTime? BIRTH { get; set; }

        [StringLength(1)]
        public string GENDER { get; set; }

        [StringLength(11)]
        public string SSN { get; set; }

        [StringLength(15)]
        public string H_PHONE { get; set; }

        [StringLength(15)]
        public string W_PHONE { get; set; }

        [StringLength(6)]
        public string W_EXT { get; set; }

        [StringLength(1)]
        public string RELATIONSHIP { get; set; }

        [StringLength(50)]
        public string EMPLOYER { get; set; }

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

        [StringLength(35)]
        public string GROUP_NO { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EFFECTIVE_DATE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EXPIRE_DATE { get; set; }

        [StringLength(35)]
        public string AUTHORIZATION_NO { get; set; }

        [StringLength(35)]
        public string POLICY_NO { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }
    }
}
