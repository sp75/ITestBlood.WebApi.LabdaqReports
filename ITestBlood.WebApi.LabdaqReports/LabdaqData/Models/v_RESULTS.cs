namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_RESULTS
    {
        public string RESULT_ALPHA { get; set; }

        public double? RESULT_NUMERIC { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal TEST_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(1)]
        public string RESULT_TYPE { get; set; }

        [StringLength(150)]
        public string TEST_NAME { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal ACC_ID { get; set; }
    }
}
