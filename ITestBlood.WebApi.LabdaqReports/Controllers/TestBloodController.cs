using System;
using Labdaq;
using System.Linq;
using System.Web.Http;

namespace ITestBlood.WebApi.LabdaqReports.Controllers
{
    [RoutePrefix("api/labdaqid")]
    public class TestBloodController : ApiController
    {
        public TestBloodController()
        {

        }


        [HttpGet, Route("")]
        public bool GetTypes(String type, String id)
        {
            bool result = false;

            using (var lab = new LabdaqClient())
            {
                switch (type.ToLower())
                {
                    case "practice":
                        result = lab.RunSql(string.Format(practices_sql, id), -1).Any();
                        break;

                    case "physician":
                        result = lab.RunSql(string.Format(physician_sql, id), -1).Any();
                        break;

                    case "patient":
                        result = lab.RunSql(string.Format(patient_sql, id), -1).Any();
                        break;
                }
            }

            return result;
        }

        const string physician_sql = @"SELECT DOC_ID FROM DOCTORS where DOC_ID = {0} ";
        const string patient_sql = @"SELECT PAT_ID FROM PATIENTS WHERE PAT_ID = '{0}' ";
        const string practices_sql = @"SELECT ORG_ID FROM ORGANIZATIONS WHERE ORG_ID = {0} ";
    }
}