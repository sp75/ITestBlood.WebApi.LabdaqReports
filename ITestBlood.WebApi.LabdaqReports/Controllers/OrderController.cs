using System;
using Labdaq;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ITestBlood.WebApi.LabdaqReports.Responses;
using System.Configuration;
using ITestBlood.WebApi.LabdaqReports.OracleImplementation;
using ITestBlood.WebApi.LabdaqReports.MsSqlImplementation;

namespace ITestBlood.WebApi.LabdaqReports.Controllers
{
    [RoutePrefix("api/labdaq")]
    public class OrderController : ApiController
    {
        private bool is_oracle_implementation { get; set; }
        private string drop_folder { get; set; }
        private string drop_folder_history { get; set; }

        public OrderController()
        {
            is_oracle_implementation = ConfigurationManager.AppSettings["Implementation"] == "oracle";
            drop_folder = ConfigurationManager.AppSettings["DROP_FOLDER"];
            drop_folder_history = ConfigurationManager.AppSettings["DROP_FOLDER_HISTORY"];
        }

        [HttpPost, Route("initialize-order")]
        public bool InitializeAccession([FromBody] OderData hl7)
        {
            try
            {
                File.WriteAllText(drop_folder + hl7.OrderNumber + ".exp", hl7.OrderData);
                File.WriteAllText(Path.Combine(drop_folder_history, hl7.OrderNumber) + ".exp1", hl7.OrderData);
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
                File.WriteAllText(drop_folder + hl7.OrderNumber + ".exp", hl7.OrderData);
                File.WriteAllText(Path.Combine(drop_folder_history, hl7.OrderNumber) + ".exp3", hl7.OrderData);
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
            if (is_oracle_implementation)
            {
                return Ok(new OrderReportOracleRepository(acc_id).GetOrderPanels());
            }
            else
            {
                return Ok(new OrderReportMsSqlRepository(acc_id).GetOrderPanelsMsSQL());
            }
        }

        [HttpPost, Route("order-status")]
        public IHttpActionResult GetOrderStatus(List<string> acc_ids)
        {
            if (is_oracle_implementation)
            {
                return Ok(new OrderStatus(acc_ids).Get());
            }
            else
            {
                return Ok(new OrderStatusMsSql(acc_ids).Get());
            }
        }
    }

    public class OderData
    {
        public string OrderNumber { get; set; }
        public string OrderData { get; set; }
    }

}