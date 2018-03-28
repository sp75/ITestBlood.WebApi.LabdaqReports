namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TEST_EVENTS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal TEST_ID { get; set; }

        [StringLength(1)]
        public string IS_FLAG { get; set; }

        [StringLength(4000)]
        public string ALPHA_TEXT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LESS_THAN { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GREATER_THAN { get; set; }

        [StringLength(4)]
        public string RESULT_CODE_ID { get; set; }

        [StringLength(3)]
        public string FLAG { get; set; }

        [StringLength(4000)]
        public string ORDER_PANELS { get; set; }

        [StringLength(1)]
        public string GENDER { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? START_AGE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? END_AGE { get; set; }

        [StringLength(1)]
        public string UNITS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SPECIES_ID { get; set; }

        [StringLength(10)]
        public string APPROVAL_REQUIRED { get; set; }

        [StringLength(4000)]
        public string POPUP_NOTES { get; set; }

        [StringLength(1)]
        public string RESULT_ACTION { get; set; }

        [StringLength(10)]
        public string FASTING { get; set; }

        public string RESULT_TRANSLATION { get; set; }
    }
}
