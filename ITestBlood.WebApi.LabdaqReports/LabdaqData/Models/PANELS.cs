namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PANELS
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal PANEL_ID { get; set; }

        [StringLength(150)]
        public string PANEL_NAME { get; set; }

        [StringLength(10)]
        public string RAPID_ORDER_CODE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FREQUENCY_DAYS { get; set; }

        [StringLength(20)]
        public string EXTERNAL_NO { get; set; }

        [StringLength(1)]
        public string PANEL_TRACKING { get; set; }

        [StringLength(1)]
        public string PRINT_HEADER { get; set; }

        [StringLength(1)]
        public string DISPLAY_ON_REPORT { get; set; }

        [StringLength(1)]
        public string EXPORT { get; set; }

        [StringLength(1)]
        public string PRINT_ON_CLOSE { get; set; }

        [StringLength(1)]
        public string SPECIMEN_TYPE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TURN_AROUND_VALUE { get; set; }

        [StringLength(1)]
        public string TURN_AROUND_UNITS { get; set; }

        [StringLength(1)]
        public string SECURITY { get; set; }

        [StringLength(1)]
        public string APPROVAL_REQUIRED { get; set; }

        [StringLength(1)]
        public string DEFAULT_REQ_STAT { get; set; }

        [StringLength(4000)]
        public string SPECIMEN_REQUIREMENTS { get; set; }

        [StringLength(4000)]
        public string NOTES { get; set; }

        [StringLength(1)]
        public string ABN_EXPERIMENTAL { get; set; }

        [StringLength(1)]
        public string PANEL_TYPE { get; set; }

        [StringLength(1)]
        public string DISPLAY_ON_REQ { get; set; }

        [StringLength(1)]
        public string HOLD_AUTO_PRINT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UNIT_COUNT { get; set; }

        [StringLength(4000)]
        public string PRINT_NOTES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SORT_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PANEL_GROUP_ID { get; set; }

        [StringLength(1)]
        public string CONSIDER_FOR_COMPLETE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MICRO_CONTAINER_ID { get; set; }

        [StringLength(10)]
        public string SPLIT_GROUP_NAME { get; set; }

        [StringLength(15)]
        public string LOINC_CODE { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NOTE_LIBRARY_ID { get; set; }
    }
}
