using Elf.Data;
using ITestBlood.WebApi.LabdaqReports.Models;
using ITestBlood.WebApi.LabdaqReports.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.MsSqlImplementation
{
    public class TestResultsMsSql
    {
        private int _acc_id { get; set; }

        public TestResultsMsSql( int acc_id)
        {
            _acc_id = acc_id;
        }

        public List<PanelResultData> Get()
        {
            var proc = new Proc(SQL_GET_TEST_RESULTS, "labdaq_mssql") { { "acc_id", _acc_id } }.All();

            return proc.Select(s => new PanelResultData
            {
                RpId = Convert.ToInt32( s["RP_ID"]),
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
                IsPositive = s["RESULT_NUMERIC"].ToDecimal() > s["HIGH_OR_SD"].ToDecimal() ? 1 : 0,
            }).ToList();
        }

        const string SQL_GET_TEST_RESULTS =
            @" select * from 
            (
                select 
                    rp.RP_ID,
                    rp.PROFILE_NAME as PANEL_NAME,
                    rp.PROFILE_ID PANEL_ID, 
                    res.CREATED_DATE, 
                    res.TEST_NO as TEST_ID, 
                    res.RESULT_NUMERIC, 
                    res.RESULT_ALPHA, 
                    rq.PRINTED_DATE,
                    (case when res.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) CORRECTED , 
                    'T' as DISPLAY_ON_REPORT, 
                    null as LOW_OR_MEAN,  
                    null as HIGH_OR_SD, 
                    res.UNITS, 
                    res.FLAG,
                    null as RESULT_CODE_TEXT, 
                    null as RESULT_TRANSLATION
                from RL_REQ_PANELS rp
                inner join RL_RESULTS res on res.rp_id = rp.rp_id and res.DEL_FLAG='F'
                inner join REQUISITIONS rq on rq.ACC_ID = rp.ACC_ID
                where rp.DEL_FLAG='F'  and rp.ACC_ID  = @acc_id
             
                union all
                select 
                    rp.RP_ID, 
                    p.PANEL_NAME, 
                    cast( rp.PANEL_ID as varchar(20)) PANEL_ID, 
                    res.CREATED_DATE, 
                    cast( res.TEST_ID as varchar(20)) TEST_ID, 
                    res.RESULT_NUMERIC, 
                    res.RESULT_ALPHA, rq.PRINTED_DATE,
                    (case when res.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) CORRECTED, 
                    res.DISPLAY_ON_REPORT,
                    res.LOW_OR_MEAN,
                    res.HIGH_OR_SD, 
                    res.UNITS, 
                    res.FLAG,
                    res.RESULT_CODE_TEXT,
                    res.RESULT_TRANSLATION
                from REQ_PANELS rp
                inner join PANELS p on rp.PANEL_ID = p.PANEL_ID
                inner join RESULTS res on  res.rp_id = rp.rp_id and res.DEL_FLAG='F'
                inner join REQUISITIONS rq on rq.ACC_ID = rp.ACC_ID
                where rp.DEL_FLAG='F' and res.DISPLAY_ON_REPORT = 'T' and rp.ACC_ID  = @acc_id 
            )x
            order by CREATED_DATE desc";
    }
}