namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_ElectronicOrders
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal AccId { get; set; }

        [StringLength(20)]
        public string PatientFirstName { get; set; }

        [StringLength(25)]
        public string PatientlastName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PatientDob { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ReceivedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ReqDate { get; set; }

        public Guid? OrderId { get; set; }
    }
}
