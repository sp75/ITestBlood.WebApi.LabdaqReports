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
using Elf.Data;

namespace ITestBlood.WebApi.LabdaqReports.Controllers
{
    [RoutePrefix("api/reqpool")]
    public class ReqpoolController : ApiController
    {
        [HttpGet, Route("take")]
        public object GetAccessionId()
        {
            try
            {
                var req = new Proc("GetReqFromPool", "lenco_ll").First<ReqPoolRespond>();

                return req;
            }
            catch
            {
                return null;
            }
        }

        public class ReqPoolRespond
        {
            public int accession_id { get; set; }
            public string labdaq_order_id { get; set; }
            public DateTime created_at { get; set; }
            public string patient_id { get; set; }
        }
    }
}