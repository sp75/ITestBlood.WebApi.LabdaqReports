using Elf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.MsSqlImplementation
{
    public class PrefSitesMsSql
    {
        private int _acc_id { get; set; }

        public PrefSitesMsSql(int acc_id)
        {
            _acc_id = acc_id;
        }

        public IEnumerable<RL_REQ_PERF_SITES> Get()
        {
            return new Proc(SQL_GET_PERF_SITE_ID, "labdaq_mssql") { { "acc_id", _acc_id } }.All<RL_REQ_PERF_SITES>();
        }

        public class RL_REQ_PERF_SITES
        {
            public string PerfSiteId { get; set; }
            public decimal RlId { get; set; }
            public string LabName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Phone { get; set; }
            public string Director { get; set; }
        }


        const string SQL_GET_PERF_SITE_ID =
  @"SELECT [PERF_SITE_ID] PerfSiteId
      ,[RL_ID] RlId
      ,[LAB_NAME] LabName
      ,[ADDRESS1] Address1
      ,[ADDRESS2] Address2
      ,[CITY] City
      ,[STATE] State
      ,[ZIP] Zip
      ,[PHONE] Phone
      ,[DIRECTOR] Director
  FROM [dbo].[RL_REQ_PERF_SITES]
  WHERE ACC_ID = @acc_id";
    }
}