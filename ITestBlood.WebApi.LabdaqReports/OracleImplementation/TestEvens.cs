using ITestBlood.WebApi.LabdaqReports.Models;
using ITestBlood.WebApi.LabdaqReports.Responses;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.OracleImplementation
{
    public class TestEvens
    {
        private LabdaqClient _lab { get; set; }
        private int _acc_id { get; set; }

        public TestEvens(LabdaqClient lab, int acc_id)
        {
            _acc_id = acc_id;
            _lab = lab;
        }

        public List<EventData> Get()
        {
            var args = new Dictionary<string, object> { { "acc_id", _acc_id } };

            return _lab.RunSql(SQL_GET_TEST_EVENTS, args).Select(s => new EventData
            {
                TestId = s["TEST_ID"].ToString(),
                ResultTranslation = s["RESULT_TRANSLATION"].ToString(),
                LessThan = s["LESS_THAN"].ToDecimal(),
                GreaterThan = s["GREATER_THAN"].ToDecimal() ,
                IsFlag = s["IS_FLAG"].ToString() == "T",
                AlphaText = s["ALPHA_TEXT"].ToString(),
                Flag = s["FLAG"].ToString(),
            }).ToList();
        }

        const string SQL_GET_TEST_EVENTS = @" select * from
         (
            SELECT
                  TO_CHAR(tst.TEST_ID ) TEST_ID , 
                  tse.LESS_THAN, 
                  tse.GREATER_THAN, 
                  tse.RESULT_TRANSLATION,
                  tse.ALPHA_TEXT,
                  tse.FLAG ,
                  tse.IS_FLAG
                FROM REQUISITIONS rq 
                INNER JOIN req_panels rp ON rq.acc_id = rp.acc_id 
                INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
                INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
                INNER JOIN tests tst ON pnt.test_id = tst.test_id 
                LEFT OUTER JOIN test_events tse ON tst.test_id = tse.test_id
                WHERE rq.ACC_ID = :acc_id  AND pnt.DISPLAY_ON_REPORT ='T' and rp.DEL_FLAG='F'
       
            union all
            SELECT  
                  res.test_no as TEST_ID, 
                  null as LESS_THAN, 
                  null as GREATER_THAN, 
                  null as RESULT_TRANSLATION,
                  TO_CHAR(res.RESULT_ALPHA) as ALPHA_TEXT,
                  res.FLAG ,
                  null  as IS_FLAG
                FROM RL_REQ_PANELS rp 
                inner join requisitions rq on rq.acc_id = rp.acc_id
                left outer join RL_RESULTS res on res.rp_id = rp.rp_id
                WHERE rq.ACC_ID= :acc_id and rp.DEL_FLAG='F'
         )";
    }
}