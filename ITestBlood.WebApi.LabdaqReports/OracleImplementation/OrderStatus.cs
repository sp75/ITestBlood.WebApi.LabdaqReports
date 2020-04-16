using ITestBlood.WebApi.LabdaqReports.Models;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.OracleImplementation
{
    public class OrderStatus
    {
        List<string> _acc_ids { get; set; }
        public OrderStatus(List<string> acc_ids)
        {
            _acc_ids = acc_ids;
        }

        public List<OrderStatusView> Get()
        {
            using (var lab = new LabdaqClient())
            {
                return lab.RunSql(String.Format(SQL_GET_ORDER, String.Join(",", _acc_ids)), -1).Select(s => new OrderStatusView
                {
                    AccId = Convert.ToInt32(s["ACC_ID"]),
                    FinalStatus = s["FINAL_STATUS"].ToString(),
                    HasAbnormalTests = new TestResults(lab, Convert.ToInt32(s["ACC_ID"])).Get().Any(a => (String.IsNullOrEmpty(a.Flag) || a.Flag == "N" ? false : true)) 
                }).ToList();
            }
        }

        const string SQL_GET_ORDER = @"SELECT ACC_ID, LD1.fnc_req_status_text(rq.ACC_ID) as FINAL_STATUS FROM REQUISITIONS rq WHERE rq.ACC_ID in ({0}) ";
    }
}