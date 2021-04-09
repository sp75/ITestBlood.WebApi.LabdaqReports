using ITestBlood.WebApi.LabdaqReports.Models;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.OracleImplementation
{
    public class OrderInfo
    {
        private LabdaqClient _lab { get; set; }
        private int _acc_id { get; set; }

        public OrderInfo(LabdaqClient lab, int acc_id)
        {
            _acc_id = acc_id;
            _lab = lab;
        }

        public OrderInfoView Get()
        {
            var args = new System.Collections.Generic.Dictionary<string, object> { { "acc_id", _acc_id } };
            var SQL_GET_ORDER = @"
                SELECT
                  rq.ACC_ID,
                  rq.PRINTED_DATE,
                  LD1.fnc_req_status_text(rq.ACC_ID) as FINAL_STATUS,
                  rq.received_date,
                  rq.NOTES,
                  rq.pat_id,
                  rq.CREATED_DATE,
                  rq.FASTING,
                  rq.DRAW_DATE 
                FROM REQUISITIONS rq 
                INNER JOIN patients pt ON rq.pat_id = pt.pat_id
                INNER JOIN doctors doc ON rq.doc_id1 = doc.doc_id
                WHERE rq.ACC_ID = :acc_id ";

            return _lab.RunSql(SQL_GET_ORDER, args).Select(s => new OrderInfoView
            {
                AccId = Convert.ToInt32(s["ACC_ID"]),
                PrintedDate = s["PRINTED_DATE"] != DBNull.Value ? (DateTime?)s["PRINTED_DATE"] : null,
                FinalStatus = s["FINAL_STATUS"].ToString(),
                Notes = s["NOTES"].ToString(),
                PatId = s["PAT_ID"].ToString(),
                CreatedDate = (DateTime)s["CREATED_DATE"],
                Fasting = s["FASTING"].ToString(),
                DrawDate = s["DRAW_DATE"] != DBNull.Value ? (DateTime?)s["DRAW_DATE"] : null,
            }).FirstOrDefault();
        }
    }
}