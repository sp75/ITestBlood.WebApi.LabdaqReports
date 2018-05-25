using Elf.Data;
using ITestBlood.WebApi.LabdaqReports.LabdaqData;
using ITestBlood.WebApi.LabdaqReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.MsSqlImplementation
{
    public class OrderInfoMsSQL
    {
        private int _acc_id { get; set; }

        public OrderInfoMsSQL(int acc_id)
        {
            _acc_id = acc_id;
        }

        public OrderInfoView Get()
        {
            var SQL_GET_ORDER = @"
                SELECT
                  cast (rq.ACC_ID as int ) AccId,
                  (select ( case when PanelCount = FinalCount then 'FINAL COPY' else 'PRELIMINARY' end ) Status  
                   from (
                           SELECT COUNT(*) as PanelCount , sum ((case when PanelStatus = 'FINAL' then 1 else 0 end ) ) as FinalCount 
                           FROM [labdaq].[dbo].[GetPanelsResult] (  rq.ACC_ID )
                        )x 
                  ) as FinalStatus,
                  rq.PRINTED_DATE PrintedDate,
                  rq.NOTES Notes,
                  rq.pat_id PatId,
                  rq.CREATED_DATE CreatedDate
                FROM REQUISITIONS rq 
                INNER JOIN patients pt ON rq.pat_id = pt.pat_id
                INNER JOIN doctors doc ON rq.doc_id1 = doc.doc_id
                WHERE rq.ACC_ID = @acc_id ";

            var proc = new Proc(SQL_GET_ORDER, "labdaq_mssql") { { "acc_id", _acc_id } }.All();
            /*            using (var db = new LabdaqEntities())
                        {
                            var re = db.Database.SqlQuery<OrderInfoView>(SQL_GET_ORDER, _acc_id).First();
                            return re;
                        }*/

            return proc.Select(s => new OrderInfoView
            {
                AccId = Convert.ToInt32(s["AccId"]),
                PrintedDate = s["PrintedDate"] != DBNull.Value ? (DateTime?)s["PrintedDate"] : null,
                FinalStatus = s["FinalStatus"].ToString(),
                Notes = s["Notes"].ToString(),
                PatId = s["PatId"].ToString(),
                CreatedDate = (DateTime)s["CreatedDate"]
            }).FirstOrDefault();
        }
    }
}