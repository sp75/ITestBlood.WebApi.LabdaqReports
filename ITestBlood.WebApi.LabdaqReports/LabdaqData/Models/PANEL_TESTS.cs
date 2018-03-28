namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PANEL_TESTS
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal PANEL_ID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal TEST_ID { get; set; }

        [StringLength(1)]
        public string DISPLAY_ON_REPORT { get; set; }

        [StringLength(1)]
        public string EXPORT { get; set; }

        [StringLength(15)]
        public string MICRO_GROUP { get; set; }

        [StringLength(1)]
        public string REQUIRED { get; set; }

        [StringLength(1)]
        public string DISPLAY_ON_ORDER { get; set; }
    }
}
