using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.OracleImplementation
{
    public class PriorHistoryTests
    {
        private LabdaqClient _lab { get; set; }
        private int _acc_id { get; set; }

        public PriorHistoryTests(LabdaqClient lab, int acc_id)
        {
            _acc_id = acc_id;
            _lab = lab;
        }

        public List<Dictionary<string, object>> Get()
        {
            var args = new Dictionary<string, object> { { "acc_id", _acc_id } };
            return _lab.RunSql(SQL_GET_TEST_RESULTS, args);
        }

        const string SQL_GET_TEST_RESULTS = @"select 
                    rq.ACC_ID, 
                    rq.doc_id1, 
                    rq.RECEIVED_DATE,
                    pnl.panel_name,
                    tst.ALPHA_RANGE_TEXT1, 
                    tst.RESULT_TYPE,
                    pnl.panel_id, 
                    tst.test_name,
                    tst.test_id, 
                    rp.rp_id, 
                    tst.PRINT_NOTES, 
                    tst.DEC_PLACES,
                    rq.PAT_ID,
                    rq.PRINTED_DATE as p_date,
                    rp.CREATED_DATE as c_date,
                    rp.NOTES as PANEL_NOTE,
                    tse.LESS_THAN, 
                    tse.GREATER_THAN, 
                    tse.RESULT_TRANSLATION,
                    tse.ALPHA_TEXT,
                    tse.FLAG as abnormal_flag,
                    tse.IS_FLAG,
                    pnt.REQUIRED as TEST_REQUIRED,
                    trim(doc.l_name ) || ', ' || trim(doc.f_name) as doc_name,
                    rp.RUN_DATE,
                    pnl.PANEL_TYPE
                from REQUISITIONS rq 
                  inner join req_panels rp ON rq.acc_id = rp.acc_id 
                  inner join panels pnl ON rp.panel_id = pnl.panel_id 
                  inner join panel_tests pnt ON rp.panel_id = pnt.panel_id 
                  inner join tests tst ON pnt.test_id = tst.test_id 
                  left outer join doctors doc ON rq.doc_id1 = doc.doc_id
                  left outer join test_events tse ON tst.test_id = tse.test_id
                where rq.ACC_ID in (
                  select ACC_ID from (
                    select rq.ACC_ID from REQUISITIONS rq 
                    inner join RL_REQ_PANELS rp ON rq.acc_id = rp.acc_id 
                    cross join (select RECEIVED_DATE, PAT_ID from REQUISITIONS where acc_id = :acc_id and rownum = 1) rq2
                    where 
                      rq.PAT_ID = rq2.PAT_ID and 
                      rq.ACC_ID <> :acc_id and 
                      LD1.fnc_req_status_text(rq.ACC_ID) like 'FINAL%' and
                      rp.profile_name = 'THIN PREP-A' and
                      rq2.RECEIVED_DATE > rq.RECEIVED_DATE
                    order by rq.RECEIVED_DATE desc
                  ) where rownum = 1
                )
                and pnl.panel_id in (5005, 5007, 6440)
                order by pnl.panel_name, tst.test_name";
    }
}