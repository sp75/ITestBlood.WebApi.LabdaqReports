using Elf.Data;
using ITestBlood.WebApi.LabdaqReports.Models;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.MsSqlImplementation
{
    public class OrderStatusMsSql
    {
        List<string> _acc_ids { get; set; }
        public OrderStatusMsSql(List<string> acc_ids)
        {
            _acc_ids = acc_ids;
        }

        public List<OrderStatusView> Get()
        {
            var result = new List<OrderStatusView>();

            foreach (var item in _acc_ids)
            {
                result.Add(new Proc(SQL_GET_ORDER, "labdaq_mssql") { { "acc_id", item } }.All().Select(s => new OrderStatusView
                {
                    AccId = Convert.ToInt32(item),
                    FinalStatus = s["FinalStatus"].ToString(),
                    HasAbnormalTests = new TestResultsMsSql(Convert.ToInt32(s["ACC_ID"])).Get().Any(a => (String.IsNullOrEmpty(a.Flag) || a.Flag == "N" ? false : true))
                }).FirstOrDefault());
            }

            return result;
        }

        const string SQL_GET_ORDER = @"select FinalStatus from dbo.GetReqStatus( @acc_id)";
    }
}