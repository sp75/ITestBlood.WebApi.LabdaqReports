namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RL_RESULTS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal RESULT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RP_ID { get; set; }

        [StringLength(1)]
        public string DEL_FLAG { get; set; }

        public double? RESULT_NUMERIC { get; set; }

        public string RESULT_ALPHA { get; set; }

        [StringLength(25)]
        public string TEST_NO { get; set; }

        [StringLength(150)]
        public string TEST_NAME { get; set; }

        [StringLength(4000)]
        public string REF_RANGE { get; set; }

        [StringLength(30)]
        public string UNITS { get; set; }

        [StringLength(3)]
        public string FLAG { get; set; }

        [StringLength(1)]
        public string STATUS { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CREATED_BY { get; set; }

        public DateTime? DELETED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DELETED_BY { get; set; }

        public string IMPORT_NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DEC_PLACES { get; set; }

        [StringLength(15)]
        public string PERF_SITE_ID { get; set; }

        [StringLength(15)]
        public string LOINC_CODE { get; set; }
    }
}
