namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TESTS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal TEST_ID { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

        [StringLength(150)]
        public string TEST_NAME { get; set; }

        [StringLength(15)]
        public string UNITS { get; set; }

        [StringLength(30)]
        public string ANALYZER_XMIT_CODE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ANALYZER_ID { get; set; }

        [StringLength(4000)]
        public string FORMULA { get; set; }

        [StringLength(1)]
        public string RESULT_TYPE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DEC_PLACES { get; set; }

        public double? PANIC_LOW { get; set; }

        public double? PANIC_HIGH { get; set; }

        [StringLength(50)]
        public string UPLOAD_XMIT_CODE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FORMAT_CODE { get; set; }

        [StringLength(30)]
        public string ALPHA_RANGE_TEXT1 { get; set; }

        [StringLength(30)]
        public string ALPHA_RANGE_TEXT2 { get; set; }

        [StringLength(30)]
        public string ALPHA_RANGE_TEXT3 { get; set; }

        public double? INVALID_LOW { get; set; }

        public double? INVALID_HIGH { get; set; }

        public double? DELTA_LOW { get; set; }

        public double? DELTA_HIGH { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DELTA_DAYS { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(255)]
        public string STORAGE_STABILITY { get; set; }

        [StringLength(35)]
        public string VOLUME_REQUIRED { get; set; }

        [StringLength(1)]
        public string BORDERLINE { get; set; }

        [StringLength(4000)]
        public string INTERFERING_SUBSTANCES { get; set; }

        [StringLength(4000)]
        public string PRINT_NOTES { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [StringLength(1)]
        public string PICK_EDIT { get; set; }

        [StringLength(1)]
        public string FLAG_ABNORMAL { get; set; }

        [StringLength(1)]
        public string ADVANCED_FORMULA { get; set; }

        [StringLength(1)]
        public string TEST_TYPE { get; set; }

        [StringLength(15)]
        public string LOINC_CODE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SORT_ID { get; set; }

        [StringLength(1)]
        public string ENABLE_AUTO_VERIFICATION { get; set; }
    }
}
