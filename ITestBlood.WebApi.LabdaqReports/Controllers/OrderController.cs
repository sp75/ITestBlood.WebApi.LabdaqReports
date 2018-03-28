using System;
using Labdaq;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ITestBlood.WebApi.LabdaqReports.Responses;

namespace ITestBlood.WebApi.LabdaqReports.Controllers
{
    [RoutePrefix("api/labdaq")]
    public class OrderController : ApiController
    {
        public OrderController()
        {

        }


        [HttpPost, Route("initialize-order")]
        public bool InitializeAccession([FromBody] OderData hl7)
        {
            try
            {
                File.WriteAllText(DROP_FOLDER + hl7.OrderNumber + ".exp", hl7.OrderData);
                File.WriteAllText(Path.Combine(@"C:\Drop\", hl7.OrderNumber) + ".exp1", hl7.OrderData);
                return true;
            }
            catch
            {
                
            }

            return false;
        }

        [HttpPost, Route("submit-order")]
        public bool SubmitOrder([FromBody] OderData hl7)
        {
            try
            {
                File.WriteAllText(DROP_FOLDER + hl7.OrderNumber + ".exp", hl7.OrderData);
                File.WriteAllText(Path.Combine(@"C:\Drop\", hl7.OrderNumber) + ".exp3", hl7.OrderData);
                return  true;
            }
            catch
            {
                
            }

            return false;
        }

        [HttpGet, Route("accession-number/{order_number}")]
        public string GetAccessionId(string order_number)
        {
            using (var lab = new LabdaqClient())
            {
                return lab.GetAccessionID(order_number);
            }
        }

        [HttpGet, Route("order-number")]
        public string GetOrderNumber()
        {
            string order_id;
            try
            {
                using (var lab = new LabdaqClient())
                {
                    order_id = lab.OrderID();
                }
            }
            catch
            {
                order_id = LabdaqClient.UniqueString(20);
            }
            return order_id;
        }

        [HttpGet, Route("order-result/{acc_id}")]
        public IHttpActionResult GetOrderPanels(int acc_id)
        {
            return Ok(new OrderReportRepository(acc_id).GetOrderPanels());
        }

        [HttpGet, Route("order-result-old/{acc_id}")]
        public IHttpActionResult GetOrderPanelsMsSql(int acc_id)
        {
            return Ok(new OrderReportRepository(acc_id).GetOrderPanelsMsSQL());
        }

        [HttpPost, Route("order-status")]
        public IHttpActionResult GetOrderPanels(List<string> acc_ids)
        {
            using (var lab = new LabdaqClient())
            {
                var SQL_GET_ORDER = @"SELECT ACC_ID, LD1.fnc_req_status_text(rq.ACC_ID) as FINAL_STATUS FROM REQUISITIONS rq WHERE rq.ACC_ID in ({0}) ";

                return Ok(lab.RunSql(String.Format(SQL_GET_ORDER, String.Join(",", acc_ids)), -1).Select(s => new 
                {
                    AccId = Convert.ToInt32(s["ACC_ID"]),
                    FinalStatus = s["FINAL_STATUS"].ToString(),
                }).ToList());
            }
        }


        const string DROP_FOLDER = @"\\192.168.1.105\Lab Orders\";
    }

    public class OderData
    {
        public string OrderNumber { get; set; }
        public string OrderData { get; set; }
    }

}