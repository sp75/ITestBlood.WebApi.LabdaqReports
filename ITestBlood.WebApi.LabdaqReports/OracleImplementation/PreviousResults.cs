using ITestBlood.WebApi.LabdaqReports.Models;
using ITestBlood.WebApi.LabdaqReports.Responses;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.OracleImplementation
{
    public class PreviousResults
    {
        private string _pat_id { get; set; }
        private  DateTime _current_date { get; set; }
        private string _test_id { get; set; }

        public PreviousResults(string pat_id, DateTime current_date, string test_id)
        {
            _pat_id = pat_id;
            _current_date = current_date;
            _test_id = test_id;
        }

        public List<PanelResultData> Get()
        {
            using (var lab = new LabdaqClient())
            {
                var str = String.Format(SQL_GET_TEST_RESULTS, _pat_id, _current_date.ToString("MM/dd/yyyy hh:mm:ss"), _test_id);
                return lab.RunSql(str, -1).Select(s => new PanelResultData
                {
                    PanelId = s["PANEL_ID"].ToString(),
                    CreatedDate = (DateTime)s["CREATED_DATE"],
                    TestId = s["TEST_ID"].ToString(),
                    ResultNumeric = s["RESULT_NUMERIC"].ToDecimal(),
                    ResultAlpha = s["RESULT_ALPHA"].ToString() == "." ? s["RESULT_CODE_TEXT"].ToString() : s["RESULT_ALPHA"].ToString(),
                    Corrected = s["CORRECTED"].ToString() == "T" ? 1 : 0,
                    DisplayOnReport = s["DISPLAY_ON_REPORT"].ToString() == "T" ? 1 : 0,
                    LowOrMean = s["LOW_OR_MEAN"].ToDecimal(),
                    HighOrSd = s["HIGH_OR_SD"].ToDecimal(),
                    Units = s["UNITS"].ToString(),
                    Flag = s["FLAG"].ToString(),
                    ResultCodeText = s["RESULT_CODE_TEXT"].ToString(),
                    ResultTranslation = s["RESULT_TRANSLATION"].ToString(),
                    AccId = Convert.ToInt32(s["ACC_ID"]),
                    ReceivedDate = s["RECEIVED_DATE"] != DBNull.Value ? (DateTime?)s["RECEIVED_DATE"] : null,
                    PrintedDate = s["PRINTED_DATE"] != DBNull.Value ? (DateTime?)s["PRINTED_DATE"] : null,
                    PanelName = s["PANEL_NAME"].ToString()
                }).ToList();
            }
        }

        const string SQL_GET_TEST_RESULTS = @"
         select * from (
            select * from 
            (
                select 
                    rp.RP_ID,
                    rp.PROFILE_NAME as PANEL_NAME,
                    rp.PROFILE_ID PANEL_ID, 
                    rp.CREATED_DATE , 
                    res.TEST_NO as TEST_ID, 
                    res.RESULT_NUMERIC, 
                    res.RESULT_ALPHA, 
                    rq.PRINTED_DATE,
                    (case when rp.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) CORRECTED , 
                    'T' as DISPLAY_ON_REPORT, 
                    null as LOW_OR_MEAN,  
                    null as HIGH_OR_SD, 
                    res.UNITS, 
                    res.FLAG,
                    null as RESULT_CODE_TEXT, 
                    null as RESULT_TRANSLATION,
                    rq.ACC_ID,
                    rq.RECEIVED_DATE
                from RL_REQ_PANELS rp
                inner join RL_RESULTS res on res.rp_id = rp.rp_id and res.DEL_FLAG='F'
                inner join REQUISITIONS rq on rq.ACC_ID = rp.ACC_ID
                where rp.DEL_FLAG='F'  and rq.PAT_ID= '{0}' and rp.CREATED_DATE < TO_DATE( '{1}', 'mm/dd/yyyy HH24:MI:SS' ) and res.TEST_NO = '{2}'
             
                union all
                select 
                    rp.RP_ID, 
                    p.PANEL_NAME, 
                    TO_CHAR( rp.PANEL_ID) PANEL_ID, 
                    rp.CREATED_DATE, 
                    TO_CHAR( res.TEST_ID ) TEST_ID, 
                    res.RESULT_NUMERIC, 
                    res.RESULT_ALPHA, 
                    rq.PRINTED_DATE,
                    (case when res.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) CORRECTED, 
                    res.DISPLAY_ON_REPORT,
                    res.LOW_OR_MEAN,
                    res.HIGH_OR_SD, 
                    res.UNITS, 
                    res.FLAG,
                    res.RESULT_CODE_TEXT,
                    res.RESULT_TRANSLATION,
                    rq.ACC_ID,
                    rq.RECEIVED_DATE
                from REQ_PANELS rp
                inner join PANELS p on rp.PANEL_ID = p.PANEL_ID
                inner join RESULTS res on  res.rp_id = rp.rp_id and res.DEL_FLAG='F'
                inner join REQUISITIONS rq on rq.ACC_ID = rp.ACC_ID
                where rp.DEL_FLAG='F' and res.DISPLAY_ON_REPORT = 'T'  and rq.PAT_ID= '{0}' and rp.CREATED_DATE < TO_DATE( '{1}', 'mm/dd/yyyy HH24:MI:SS' ) and  TO_CHAR( res.TEST_ID ) = '{2}'
            )
            order by CREATED_DATE desc
          )  where  ROWNUM <= 6";

    }
}