using Elf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.MsSqlImplementation
{
    public class PriorHistoryTestsMsSql
    {

        private int _acc_id { get; set; }

        public PriorHistoryTestsMsSql( int acc_id)
        {
            _acc_id = acc_id;
        }

        public JsonTable Get()
        {
            var args = new Dictionary<string, object> {
                    { "acc_id", _acc_id}
                };

            return new Proc(SQL_GET_TEST_RESULTS, "labdaq_mssql") { { "acc_id", _acc_id } }.All();
        }

        const string SQL_GET_TEST_RESULTS =
                  @"select 
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
                    (doc.l_name  + ', ' + doc.f_name) as doc_name,
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
                    select top 1 rq.ACC_ID from REQUISITIONS rq 
                    inner join RL_REQ_PANELS rp ON rq.acc_id = rp.acc_id 
                    cross join (select top 1 RECEIVED_DATE, PAT_ID from REQUISITIONS where acc_id = @acc_id ) rq2
                    where 
                      rq.PAT_ID = rq2.PAT_ID and 
                      rq.ACC_ID <> @acc_id and 
                      
                      rp.profile_name = 'THIN PREP-A' and
                      rq2.RECEIVED_DATE > rq.RECEIVED_DATE
                      order by rq.RECEIVED_DATE desc
                )
                and pnl.panel_id in (5005, 5007, 6440)
                order by pnl.panel_name, tst.test_name";
    }
}