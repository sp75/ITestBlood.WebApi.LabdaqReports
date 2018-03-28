namespace ITestBlood.WebApi.LabdaqReports.LabdaqData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LabdaqEntities : DbContext
    {
        public LabdaqEntities()
            : base("name=LabdaqEntities")
        {
        }

        public virtual DbSet<DISTRICTS> DISTRICTS { get; set; }
        public virtual DbSet<DOCTOR_DETAILS> DOCTOR_DETAILS { get; set; }
        public virtual DbSet<DOCTORS> DOCTORS { get; set; }
        public virtual DbSet<INS_COMPANIES> INS_COMPANIES { get; set; }
        public virtual DbSet<INS_COMPANIES_DETAILS> INS_COMPANIES_DETAILS { get; set; }
        public virtual DbSet<ORGANIZATIONS> ORGANIZATIONS { get; set; }
        public virtual DbSet<ORGANIZATIONS_DETAILS> ORGANIZATIONS_DETAILS { get; set; }
        public virtual DbSet<PANEL_TESTS> PANEL_TESTS { get; set; }
        public virtual DbSet<PANELS> PANELS { get; set; }
        public virtual DbSet<PAT_INSURANCE> PAT_INSURANCE { get; set; }
        public virtual DbSet<PATIENTS> PATIENTS { get; set; }
        public virtual DbSet<REGIONS> REGIONS { get; set; }
        public virtual DbSet<REQ_PANELS> REQ_PANELS { get; set; }
        public virtual DbSet<REQUISITION_DETAILS> REQUISITION_DETAILS { get; set; }
        public virtual DbSet<REQUISITIONS> REQUISITIONS { get; set; }
        public virtual DbSet<RESULTS> RESULTS { get; set; }
        public virtual DbSet<RL_REQ_PANEL_CPTS> RL_REQ_PANEL_CPTS { get; set; }
        public virtual DbSet<RL_REQ_PANELS> RL_REQ_PANELS { get; set; }
        public virtual DbSet<RL_RESULTS> RL_RESULTS { get; set; }
        public virtual DbSet<TESTS> TESTS { get; set; }
        public virtual DbSet<USER_INFO> USER_INFO { get; set; }
        public virtual DbSet<REQ_PANEL_CPTS> REQ_PANEL_CPTS { get; set; }
        public virtual DbSet<TEST_EVENTS> TEST_EVENTS { get; set; }
        public virtual DbSet<v_Doctors> v_Doctors { get; set; }
        public virtual DbSet<v_ElectronicOrders> v_ElectronicOrders { get; set; }
        public virtual DbSet<v_INS_COMPANIES> v_INS_COMPANIES { get; set; }
        public virtual DbSet<v_PATIENTS> v_PATIENTS { get; set; }
        public virtual DbSet<v_REQUISITIONS> v_REQUISITIONS { get; set; }
        public virtual DbSet<v_RESULTS> v_RESULTS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DISTRICTS>()
                .Property(e => e.DISTRICT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DISTRICTS>()
                .Property(e => e.RPT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTOR_DETAILS>()
                .Property(e => e.DOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTORS>()
                .Property(e => e.DOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTORS>()
                .Property(e => e.ORG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTORS>()
                .Property(e => e.RPT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTORS>()
                .Property(e => e.DISTRICT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTORS>()
                .Property(e => e.REGION_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DOCTORS>()
                .Property(e => e.DEFAULT_REQ_LOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<INS_COMPANIES>()
                .Property(e => e.INS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<INS_COMPANIES>()
                .Property(e => e.IG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<INS_COMPANIES>()
                .HasOptional(e => e.INS_COMPANIES_DETAILS)
                .WithRequired(e => e.INS_COMPANIES)
                .WillCascadeOnDelete();

            modelBuilder.Entity<INS_COMPANIES_DETAILS>()
                .Property(e => e.InsId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ORGANIZATIONS>()
                .Property(e => e.ORG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ORGANIZATIONS>()
                .Property(e => e.RPT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ORGANIZATIONS>()
                .Property(e => e.ORG_TYPE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ORGANIZATIONS>()
                .HasOptional(e => e.ORGANIZATIONS_DETAILS)
                .WithRequired(e => e.ORGANIZATIONS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ORGANIZATIONS_DETAILS>()
                .Property(e => e.ORG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANEL_TESTS>()
                .Property(e => e.PANEL_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANEL_TESTS>()
                .Property(e => e.TEST_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.PANEL_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.FREQUENCY_DAYS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.TURN_AROUND_VALUE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.UNIT_COUNT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.SORT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.PANEL_GROUP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.MICRO_CONTAINER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PANELS>()
                .Property(e => e.NOTE_LIBRARY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PAT_INSURANCE>()
                .Property(e => e.PI_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PAT_INSURANCE>()
                .Property(e => e.INS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PAT_INSURANCE>()
                .Property(e => e.PRIORITY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.PAT_ID_NUMERIC)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.CONTROL_LEVEL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.REIMBURSE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.DOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.SPECIES_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.ANALYZER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.ABN_RPT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.ORG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.DISTRICT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.REGION_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<PATIENTS>()
                .Property(e => e.CONTROL_LOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGIONS>()
                .Property(e => e.REGION_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REGIONS>()
                .Property(e => e.RPT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.ACC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.SG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.PANEL_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.LOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.CREATED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.DELETED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.APPROVED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.LD_REFLEX_RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.ACCEPTED_BY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.RUN_BY_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANELS>()
                .Property(e => e.UNIT_COUNT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITION_DETAILS>()
                .Property(e => e.AccId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.ACC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_ID1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_OPT1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_ID2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_OPT2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_ID3)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_OPT3)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_ID4)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DOC_OPT4)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.LOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.PI_ID1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.PI_ID2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.PI_ID3)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.CREATED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DELETED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.WS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.PV_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.ORG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DISTRICT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.REGION_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.SPLIT_ACC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.REGION_OPT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.DISTRICT_OPT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.ORG_OPT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .Property(e => e.LOC_OPT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQUISITIONS>()
                .HasOptional(e => e.REQUISITION_DETAILS)
                .WithRequired(e => e.REQUISITIONS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.RESULT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.TEST_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.CREATED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.DELETED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.ISOLATE_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.MEDIA_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RESULTS>()
                .Property(e => e.ANALYZER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANEL_CPTS>()
                .Property(e => e.RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.ACC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.RL_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.CREATED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.DELETED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.REFLEX_RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.LD_REFLEX_RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.SG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_REQ_PANELS>()
                .Property(e => e.UNIT_COUNT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_RESULTS>()
                .Property(e => e.RESULT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_RESULTS>()
                .Property(e => e.RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_RESULTS>()
                .Property(e => e.CREATED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_RESULTS>()
                .Property(e => e.DELETED_BY)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RL_RESULTS>()
                .Property(e => e.DEC_PLACES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TESTS>()
                .Property(e => e.TEST_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TESTS>()
                .Property(e => e.ANALYZER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TESTS>()
                .Property(e => e.DEC_PLACES)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TESTS>()
                .Property(e => e.FORMAT_CODE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TESTS>()
                .Property(e => e.DELTA_DAYS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TESTS>()
                .Property(e => e.SORT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.USER_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.WINDOW_COUNT)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.LOG_OFF_MINS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.PASSWORD_EXPIRE_DAYS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.LOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.SIGNATURE_ATTACH_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.APPROVAL_PANEL_SET_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USER_INFO>()
                .Property(e => e.PRINTER_MAPPING_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<REQ_PANEL_CPTS>()
                .Property(e => e.RP_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.TEST_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.LESS_THAN)
                .HasPrecision(15, 2);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.GREATER_THAN)
                .HasPrecision(15, 2);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.START_AGE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.END_AGE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.SPECIES_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TEST_EVENTS>()
                .Property(e => e.APPROVAL_REQUIRED)
                .IsFixedLength();

            modelBuilder.Entity<v_Doctors>()
                .Property(e => e.DOC_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_Doctors>()
                .Property(e => e.DISTRICT_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_Doctors>()
                .Property(e => e.ORG_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_Doctors>()
                .Property(e => e.ACTIVE)
                .IsUnicode(false);

            modelBuilder.Entity<v_ElectronicOrders>()
                .Property(e => e.AccId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_INS_COMPANIES>()
                .Property(e => e.INS_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.AccId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.DocId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.DoctorDistrictId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.OrgId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.InsId1)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.InsId2)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.InsId3)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_REQUISITIONS>()
                .Property(e => e.SalesRepId)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_RESULTS>()
                .Property(e => e.TEST_ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_RESULTS>()
                .Property(e => e.ACC_ID)
                .HasPrecision(38, 0);
        }
    }
}
