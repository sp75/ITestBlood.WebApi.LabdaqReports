namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RESULTS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal RESULT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RP_ID { get; set; }

        [StringLength(1)]
        public string DEL_FLAG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TEST_ID { get; set; }

        public double? RESULT_NUMERIC { get; set; }

        public string RESULT_ALPHA { get; set; }

        [StringLength(4)]
        public string RESULT_CODE_ID { get; set; }

        [StringLength(400)]
        public string RESULT_CODE_TEXT { get; set; }

        [StringLength(3)]
        public string FLAG { get; set; }

        [StringLength(1)]
        public string DELTA_FLAG { get; set; }

        public double? LOW_OR_MEAN { get; set; }

        public double? HIGH_OR_SD { get; set; }

        [StringLength(20)]
        public string LOT_NAME { get; set; }

        public DateTime? LOT_EXPIRE { get; set; }

        [StringLength(1)]
        public string DO_NOT_REPORT { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CREATED_BY { get; set; }

        public DateTime? DELETED_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DELETED_BY { get; set; }

        [StringLength(1)]
        public string RERUN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ISOLATE_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MEDIA_ID { get; set; }

        public string CORRECTIVE_ACTION { get; set; }

        [StringLength(1)]
        public string DISPLAY_ON_REPORT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ANALYZER_ID { get; set; }

        [StringLength(255)]
        public string ANALYZER_XMIT_NAME { get; set; }

        public double? OPTIMAL_LOW { get; set; }

        public double? OPTIMAL_HIGH { get; set; }

        public string RESULT_TRANSLATION { get; set; }

        [StringLength(15)]
        public string UNITS { get; set; }
    }
}
