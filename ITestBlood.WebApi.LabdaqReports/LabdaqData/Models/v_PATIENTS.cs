namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PATIENTS
    {
        [Key]
        [StringLength(30)]
        public string PAT_ID { get; set; }

        [StringLength(25)]
        public string L_NAME { get; set; }

        [StringLength(20)]
        public string F_NAME { get; set; }

        [StringLength(1)]
        public string ACTIVE { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BIRTH { get; set; }

        public int? AGE { get; set; }

        [StringLength(1)]
        public string GENDER { get; set; }
    }
}
