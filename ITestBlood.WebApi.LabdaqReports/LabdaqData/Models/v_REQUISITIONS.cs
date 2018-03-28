namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_REQUISITIONS
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

        [Column(TypeName = "numeric")]
        public decimal? DocId { get; set; }

        [StringLength(20)]
        public string DoctorFirstName { get; set; }

        [StringLength(25)]
        public string DoctorlastName { get; set; }

        [StringLength(15)]
        public string DoctorCellPhone { get; set; }

        [StringLength(15)]
        public string DoctorWorkPhone { get; set; }

        [StringLength(100)]
        public string DoctorAddress { get; set; }

        [StringLength(50)]
        public string DoctorAddress2 { get; set; }

        [StringLength(25)]
        public string DoctorCity { get; set; }

        [StringLength(10)]
        public string DoctorZip { get; set; }

        [StringLength(2)]
        public string DoctorState { get; set; }

        [StringLength(50)]
        public string OrgName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DoctorDistrictId { get; set; }

        [StringLength(20)]
        public string DoctorExternalNo { get; set; }

        [StringLength(50)]
        public string InsuranceName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ReceivedDate { get; set; }

        [StringLength(35)]
        public string PolicyNo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ReqDate { get; set; }

        [StringLength(30)]
        public string PatId { get; set; }

        public string SpecimenOrderNote { get; set; }

        [StringLength(50)]
        public string OrgAddress { get; set; }

        [StringLength(25)]
        public string OrgCity { get; set; }

        [StringLength(2)]
        public string OrgState { get; set; }

        [StringLength(15)]
        public string OrgPhone { get; set; }

        [StringLength(30)]
        public string OrgFax { get; set; }

        [StringLength(50)]
        public string DistrictName { get; set; }

        [StringLength(50)]
        public string ServiceRep { get; set; }

        [StringLength(250)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(50)]
        public string SalesRep { get; set; }

        [StringLength(1)]
        public string DelFlag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OrgId { get; set; }

        [StringLength(50)]
        public string InsuranceName2 { get; set; }

        [StringLength(35)]
        public string PolicyNo2 { get; set; }

        [StringLength(50)]
        public string InsuranceName3 { get; set; }

        [StringLength(35)]
        public string PolicyNo3 { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PrintedDate { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DrawDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InsId1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InsId2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InsId3 { get; set; }

        [StringLength(47)]
        public string PatientName { get; set; }

        [StringLength(47)]
        public string DoctorName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SalesRepId { get; set; }
    }
}
