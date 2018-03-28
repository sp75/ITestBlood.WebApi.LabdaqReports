using Elf.Data;
using ITestBlood.WebApi.LabdaqReports.LabdaqData;
using ITestBlood.WebApi.LabdaqReports.Models;
using ITestBlood.WebApi.LabdaqReports.MsSqlImplementation;
using ITestBlood.WebApi.LabdaqReports.OracleImplementation;
using ITestBlood.WebApi.LabdaqReports.Settings;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITestBlood.WebApi.LabdaqReports.Responses
{
    public class OrderReportRepository
    {
        private int _acc_id { get; set; }

        public OrderReportRepository(int acc_id)
        {
            _acc_id = acc_id;
        }

        public object GetOrderPanels()
        {
            using (var lab = new LabdaqClient())
            {
                var order_info = new OrderInfo(lab, _acc_id).Get();

                var args = new System.Collections.Generic.Dictionary<string, object> {
                    { "acc_id", _acc_id}
                };

                var sql = @"
        select * from
        (
            SELECT
                  pnl.PANEL_NAME,
                  tst.ALPHA_RANGE_TEXT1, 
                  tst.RESULT_TYPE,
                  TO_CHAR( pnl.PANEL_ID ) PANEL_ID, 
                  tst.TEST_NAME,
                  TO_CHAR(tst.TEST_ID ) TEST_ID , 
                  tst.PRINT_NOTES, 
                  tst.DEC_PLACES,
                  rp.CREATED_DATE,
                  rp.NOTES as PANEL_NOTE,
                  pnt.REQUIRED as TEST_REQUIRED,
                  rp.RUN_DATE,
                  pnl.PANEL_TYPE,
                  (case when rp.RUN_DATE is null then 'PRELIMINARY' else 'FINAL' end) PANEL_STATUS ,
                  (case when rp.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) ADDED_AFTER_PRINT,
                  tst.STORAGE_STABILITY 
                FROM req_panels rp   
                INNER JOIN REQUISITIONS rq ON rq.acc_id = rp.acc_id 
                INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
                INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
                INNER JOIN tests tst ON pnt.test_id = tst.test_id 
                WHERE rq.ACC_ID = :acc_id  AND pnt.DISPLAY_ON_REPORT ='T' and rp.DEL_FLAG='F'
       
         union all
         SELECT 
                    rp.PROFILE_NAME as PANEL_NAME,
                    res.REF_RANGE as ALPHA_RANGE_TEXT1, 
                    '' as RESULT_TYPE,
                    rp.PROFILE_ID PANEL_ID, 
                    res.TEST_NAME,
                    res.test_no as TEST_ID, 
                    TO_CHAR( res.IMPORT_NOTES) as PRINT_NOTES, 
                    res.DEC_PLACES,
                    rp.CREATED_DATE,
                    rp.NOTES as PANEL_NOTE,
                    'T' as TEST_REQUIRED,
                    rp.RUN_DATE,
                    '' as PANEL_TYPE,
                    (case when rp.RUN_DATE is null then 'PRELIMINARY' else 'FINAL' end) PANEL_STATUS ,
                    (case when rp.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) ADDED_AFTER_PRINT,
                    '' as STORAGE_STABILITY
                FROM RL_REQ_PANELS rp 
                inner join requisitions rq on rq.acc_id = rp.acc_id
                left outer join RL_RESULTS res on res.rp_id = rp.rp_id
                WHERE rq.ACC_ID= :acc_id and rp.DEL_FLAG='F'
        )
        ORDER BY  panel_name ASC, test_id ASC, test_name ASC";

                var test_result = new TestResults(lab, _acc_id).Get();
                var evetns = new TestEvens(lab, _acc_id).Get();
                var drugs = new DrugPanels(_acc_id).Get();

                var lab_result = new List<OrderResultData>();
                lab.RunSql(sql, args).Select(s => new OrderResultData
                {
                    CreatedDate = (DateTime)s["CREATED_DATE"],
                    PanelId = s["PANEL_ID"].ToString(),
                    PanelName = s["PANEL_NAME"].ToString(),
                    PanelType = s["PANEL_TYPE"].ToString(),
                    PanelStatus = s["PANEL_STATUS"].ToString(),
                    PanelNotes = s["PANEL_NOTE"].ToString(),
                    TestName = s["TEST_NAME"].ToString(),
                    TestId = s["TEST_ID"].ToString(),
                    PrintNotes = RemoveControlCharacters(s["PRINT_NOTES"].ToString()),
                    AddedAfterPrint = s["PRINT_NOTES"].ToString() == "T" ? 1 : 0,
                    TestResultType = s["RESULT_TYPE"].ToString(),
                    DecPlaces = s["DEC_PLACES"].ToInt32(),
                    AlphaRangeText = s["ALPHA_RANGE_TEXT1"].ToString(),
                    IsAllergen = new Regex(@"\[[A-Z]\d{1,3}\]").IsMatch(s["TEST_NAME"].ToString()) ? 1 : 0,
                    StorageStability = s["STORAGE_STABILITY"].ToString(),
                }).ToList().ForEach(item =>
                {
                    if (!lab_result.Select(s => s.TestId).Contains(item.TestId))
                    {
                        lab_result.Add(item);
                    }
                });

                int STINoteIndex = 0;
                var panels = lab_result.GroupBy(g => new { g.PanelId, g.PanelName, g.PanelType, g.PanelStatus, g.PanelNotes, g.AddedAfterPrint, g.CreatedDate }).Select(s => new
                {
                    s.Key.PanelId,
                    s.Key.PanelName,
                    IsRx = s.Key.PanelName.Contains("(Rx)") ? 1 : 0,
                    IsSTI = Tests.STITests.Contains(s.Key.PanelId),
                    s.Key.PanelType,
                    s.Key.PanelStatus,
                    s.Key.PanelNotes,
                    s.Key.AddedAfterPrint,
                    s.Key.CreatedDate,
                    Tests = s.Select(t => new
                    {
                        t.TestId,
                        t.TestName,
                        t.PrintNotes,
                        Reference = InsertReference(t, test_result.Where(w => w.PanelId == s.Key.PanelId && w.TestId == t.TestId).ToList()),
                        STINoteIndex = Tests.STITests.Contains(s.Key.PanelId) && !String.IsNullOrEmpty(t.PrintNotes) ? (int?)++STINoteIndex : null,
                        t.IsAllergen,
                        t.StorageStability,
                        Results = new
                        {
                            Current = test_result.Where(w => w.PanelId == s.Key.PanelId && w.TestId == t.TestId).Select(r => new
                                {
                                    r.CreatedDate,
                                    r.ResultNumeric,
                                    r.Flag,
                                    r.Units,
                                    CutOff = r.HighOrSd != null ? Math.Round(r.HighOrSd.Value) : 0,
                                    r.HighOrSd,
                                    r.IsPositive,
                                    r.Corrected,
                                    AllergenLevel = t.IsAllergen == 1 ? GetAllergenLevel(r.ResultNumeric) : null,
                                    AllergenLevelPercent = t.IsAllergen == 1 ? (int?)Convert.ToInt32((100 * r.ResultNumeric) / GetLessByLevel(r.ResultNumeric)) : null,
                                    IsAbnormalFlag = (String.IsNullOrEmpty(r.Flag) || r.Flag == "N" ? 0 : 1),
                                    ResultType = GetResultType(r.Flag),
                                    r.ResultCodeText,
                                    r.ResultAlpha,
                                    Result = GetResult(evetns.Where(ev => ev.TestId == t.TestId).ToList(), r, t),
                                    Outcome = new string[] { "S", "D" }.Contains(s.Key.PanelType) ? (r.IsPositive == 1 ? "POSITIVE" : "NEGATIVE") : "",
                                    IsInconsistentResult = (drugs.Consistent.Any(a => a.TestId == t.TestId) || (!drugs.Inconsistent1.Any(a => a.TestId == t.TestId) && r.ResultNumeric <= r.HighOrSd)) ? 0 : 1
                                }).FirstOrDefault(),
                            Previous = new PreviousResults(order_info.PatId, s.Key.CreatedDate, t.TestId).Get().Select(r => new
                            {
                                r.AccId,
                                r.CreatedDate,
                                r.ResultNumeric,
                                r.Flag,
                                r.Units,
                                CutOff = r.HighOrSd != null ? Math.Round(r.HighOrSd.Value) : 0,
                                r.HighOrSd,
                                IsPositive = r.ResultNumeric > r.HighOrSd ? 1 : 0,
                                r.Corrected,
                                AllergenLevel = t.IsAllergen == 1 ? GetAllergenLevel(r.ResultNumeric) : null,
                                AllergenLevelPercent = t.IsAllergen == 1 ? (100 * r.ResultNumeric) / GetLessByLevel(r.ResultNumeric) : null,
                                IsAbnormalFlag = (String.IsNullOrEmpty(r.Flag) || r.Flag == "N" ? 0 : 1),
                                ResultType = GetResultType(r.Flag),
                                r.ResultCodeText,
                                r.ResultAlpha,
                                Result = GetResult(evetns.Where(ev => ev.TestId == t.TestId).ToList(), r, t)
                            }).ToList()
                        }
                    })
                }).ToList();

                var final_diagnosis = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "991002A" /*Final Diagnosis*/).Select(s => s.ResultAlpha).FirstOrDefault();
                var specimen_adequacy = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "992002A" /*Specimen Adequacy*/).Select(s => s.ResultAlpha).FirstOrDefault();
                var gross_description = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990011A" /*Gross Description*/).Select(s => s.ResultAlpha).FirstOrDefault();
                var prior_history = new PriorHistoryTests(lab, _acc_id).Get();

                bool IsOBGYN = true;
                test_result.ForEach(f => { if (!Tests.OBGYN.Contains(f.PanelId)) { IsOBGYN = false; return; } });
                var result = new
                {
                    AllTestsIsOBGYN = IsOBGYN,
                    AllTestsIsDrugs = lab_result.Any(a => a.PanelType != "D" && a.PanelType != "S") ? 0 : 1,
                    HasAllergenTests = lab_result.Any(a => a.IsAllergen == 1) ? 1 : 0,
                    HasDrugsTests = lab_result.Any(a => a.PanelType == "D" || a.PanelType == "S") ? 1 : 0,
                    HasAbnormalTests = test_result.Any(a => (String.IsNullOrEmpty(a.Flag) || a.Flag == "N" ? 0 : 1) == 1) ? 1 : 0,
                    HasSTITests = test_result.Any(a => Tests.STITests.Contains(a.TestId)),
                    HasThinPrepPanels = test_result.Any(a => new string[] { "2975", "2975-2" }.Contains(a.PanelId)),
                    Order = order_info,
                    Panels = panels,
                    Drugs = drugs,
                    GynCytology = new
                    {
                        Diagnosis = !String.IsNullOrEmpty(final_diagnosis) ? final_diagnosis : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "991002A").Select(s => s.PrintNotes).FirstOrDefault(),
                        Adequacy = !String.IsNullOrEmpty(specimen_adequacy) ? specimen_adequacy : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "992002A").Select(s => s.PrintNotes).FirstOrDefault(),
                        Comments = !String.IsNullOrEmpty(gross_description) ? gross_description : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990011A").Select(s => s.PrintNotes).FirstOrDefault(),
                        TestOrdered = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990004A" /*Gross*/).Select(s => s.ResultAlpha).FirstOrDefault(),
                        ClinicalInformation = test_result.Where(w => w.PanelId == "2975" && w.TestId == "993001" /*ClinicalHistory*/).OrderByDescending(o=> o.CreatedDate).Select(s => s.ResultAlpha).FirstOrDefault(),
                        PriorHistory = prior_history.Any() ? new PreviousResults(order_info.PatId, order_info.CreatedDate, "991002A").Get().Select(r => new
                                                {
                                                    SpecimenNumber = r.AccId,
                                                    ReceivedDate = r.ReceivedDate,
                                                    ReportDate = r.PrintedDate,
                                                    ReportType = r.PanelName,
                                                    Diagnosis = r.ResultAlpha,
                                                    Test = prior_history
                                                }).ToList() : null
                    }
                };

                return result;
            }
        }

        public JsonTable GetPriorHistoryTestsMsSQL(int acc_id)
        {
            var args = new System.Collections.Generic.Dictionary<string, object> {
                    { "acc_id", acc_id}
                };

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

            return new Proc(SQL_GET_TEST_RESULTS, "labdaq_mssql") { { "acc_id", acc_id } }.All();
        }

        public object GetOrderPanelsMsSQL()
        {
            var order_info = new OrderInfoMsSQL(_acc_id).Get();

            var sql = @"
        select * from
        (
            SELECT
                  pnl.PANEL_NAME,
                  tst.ALPHA_RANGE_TEXT1, 
                  tst.RESULT_TYPE,
                  cast( pnl.PANEL_ID as varchar(20) ) PANEL_ID, 
                  tst.TEST_NAME,
                  cast(tst.TEST_ID as varchar(20)) TEST_ID , 
                  tst.PRINT_NOTES, 
                  tst.DEC_PLACES,
                  rp.CREATED_DATE,
                  rp.NOTES as PANEL_NOTE,
                  pnt.REQUIRED as TEST_REQUIRED,
                  rp.RUN_DATE,
                  pnl.PANEL_TYPE,
                  (case when rp.RUN_DATE is null then 'PRELIMINARY' else 'FINAL' end) PANEL_STATUS ,
                  (case when rp.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) ADDED_AFTER_PRINT,
                  tst.STORAGE_STABILITY 
                FROM req_panels rp   
                INNER JOIN REQUISITIONS rq ON rq.acc_id = rp.acc_id 
                INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
                INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
                INNER JOIN tests tst ON pnt.test_id = tst.test_id 
                WHERE rq.ACC_ID = @acc_id  AND pnt.DISPLAY_ON_REPORT ='T' and rp.DEL_FLAG='F'
       
         union all
         SELECT 
                    rp.PROFILE_NAME as PANEL_NAME,
                    res.REF_RANGE as ALPHA_RANGE_TEXT1, 
                    '' as RESULT_TYPE,
                    rp.PROFILE_ID PANEL_ID, 
                    res.TEST_NAME,
                    res.test_no as TEST_ID, 
                    res.IMPORT_NOTES as PRINT_NOTES, 
                    res.DEC_PLACES,
                    rp.CREATED_DATE,
                    rp.NOTES as PANEL_NOTE,
                    'T' as TEST_REQUIRED,
                    rp.RUN_DATE,
                    '' as PANEL_TYPE,
                    (case when rp.RUN_DATE is null then 'PRELIMINARY' else 'FINAL' end) PANEL_STATUS ,
                    (case when rp.CREATED_DATE > rq.PRINTED_DATE then 'T' else 'F' end) ADDED_AFTER_PRINT,
                    '' as STORAGE_STABILITY
                FROM RL_REQ_PANELS rp 
                inner join requisitions rq on rq.acc_id = rp.acc_id
                left outer join RL_RESULTS res on res.rp_id = rp.rp_id
                WHERE rq.ACC_ID= @acc_id and rp.DEL_FLAG='F'
        )x
        ORDER BY  panel_name ASC, test_id ASC, test_name ASC";

            var test_result = new TestResultsMsSql(_acc_id).Get();
            var evetns = new TestEvensMsSql(_acc_id).Get();
            var drugs = new MsSqlImplementation.DrugPanelsMsSql(_acc_id).Get();

            var proc = new Proc(sql, "labdaq_mssql") { { "acc_id", _acc_id } }.All();

            var lab_result = new List<OrderResultData>();
            proc.Select(s => new OrderResultData
            {
                CreatedDate = (DateTime)s["CREATED_DATE"],
                PanelId = s["PANEL_ID"].ToString(),
                PanelName = s["PANEL_NAME"].ToString(),
                PanelType = s["PANEL_TYPE"].ToString(),
                PanelStatus = s["PANEL_STATUS"].ToString(),
                PanelNotes = s["PANEL_NOTE"].ToString(),
                TestName = s["TEST_NAME"].ToString(),
                TestId = s["TEST_ID"].ToString(),
                PrintNotes = RemoveControlCharacters(s["PRINT_NOTES"].ToString()),
                AddedAfterPrint = s["PRINT_NOTES"].ToString() == "T" ? 1 : 0,
                TestResultType = s["RESULT_TYPE"].ToString(),
                DecPlaces = s["DEC_PLACES"].ToInt32(),
                AlphaRangeText = s["ALPHA_RANGE_TEXT1"].ToString(),
                IsAllergen = new Regex(@"\[[A-Z]\d{1,3}\]").IsMatch(s["TEST_NAME"].ToString()) ? 1 : 0,
                StorageStability = s["STORAGE_STABILITY"].ToString(),
            }).ToList().ForEach(item =>
            {
                if (!lab_result.Select(s => s.TestId).Contains(item.TestId))
                {
                    lab_result.Add(item);
                }
            }); 

            int STINoteIndex = 0;
            var panels = lab_result.GroupBy(g => new
            {
                g.PanelId,
                g.PanelName,
                g.PanelType,
                g.PanelStatus,
                g.PanelNotes,
                g.AddedAfterPrint,
                g.CreatedDate
            }).Select(s => new
            {
                s.Key.PanelId,
                s.Key.PanelName,
                IsRx = s.Key.PanelName.Contains("(Rx)") ? 1 : 0,
                IsSTI = Tests.STITests.Contains(s.Key.PanelId),
                s.Key.PanelType,
                s.Key.PanelStatus,
                s.Key.PanelNotes,
                s.Key.AddedAfterPrint,
                s.Key.CreatedDate,
                Tests = s.Select(t => new
                {
                    t.TestId,
                    t.TestName,
                    t.PrintNotes,
                    Reference = InsertReference(t, test_result.Where(w => w.PanelId == s.Key.PanelId && w.TestId == t.TestId).ToList()),
                    STINoteIndex = Tests.STITests.Contains(s.Key.PanelId) && !String.IsNullOrEmpty(t.PrintNotes) ? (int?)++STINoteIndex : null,
                    t.IsAllergen,
                    t.StorageStability,
                    Results = new
                    {
                        Current = test_result.Where(w => w.PanelId == s.Key.PanelId && w.TestId == t.TestId).Select(r => new
                        {
                            r.CreatedDate,
                            r.ResultNumeric,
                            r.Flag,
                            r.Units,
                            CutOff = r.HighOrSd != null ? Math.Round(r.HighOrSd.Value) : 0,
                            r.HighOrSd,
                            r.IsPositive,
                            r.Corrected,
                            AllergenLevel = t.IsAllergen == 1 ? GetAllergenLevel(r.ResultNumeric) : null,
                            AllergenLevelPercent = t.IsAllergen == 1 ? (int?)Convert.ToInt32((100 * r.ResultNumeric) / GetLessByLevel(r.ResultNumeric)) : null,
                            IsAbnormalFlag = (String.IsNullOrEmpty(r.Flag) || r.Flag == "N" ? 0 : 1),
                            ResultType = GetResultType(r.Flag),
                            r.ResultCodeText,
                            r.ResultAlpha,
                            Result = GetResult(evetns.Where(ev => ev.TestId == t.TestId).ToList(), r, t),
                            Outcome = new string[] { "S", "D" }.Contains(s.Key.PanelType) ? (r.IsPositive == 1 ? "POSITIVE" : "NEGATIVE") : "",
                            IsInconsistentResult = (drugs.Consistent.Any(a => a.TestId == t.TestId) || (!drugs.Inconsistent1.Any(a => a.TestId == t.TestId) && r.ResultNumeric <= r.HighOrSd)) ? 0 : 1
                        }).FirstOrDefault(),
                        Previous = new PreviousResultsMsSql(order_info.PatId, s.Key.CreatedDate.ToServerTime(), t.TestId).Get().Select(r => new
                        {
                            r.CreatedDate,
                            r.ResultNumeric,
                            r.Flag,
                            r.Units,
                            CutOff = r.HighOrSd != null ? Math.Round(r.HighOrSd.Value) : 0,
                            r.HighOrSd,
                            IsPositive = r.ResultNumeric > r.HighOrSd ? 1 : 0,
                            r.Corrected,
                            AllergenLevel = t.IsAllergen == 1 ? GetAllergenLevel(r.ResultNumeric) : null,
                            AllergenLevelPercent = t.IsAllergen == 1 ? (100 * r.ResultNumeric) / GetLessByLevel(r.ResultNumeric) : null,
                            IsAbnormalFlag = (String.IsNullOrEmpty(r.Flag) || r.Flag == "N" ? 0 : 1),
                            ResultType = GetResultType(r.Flag),
                            r.ResultCodeText,
                            r.ResultAlpha,
                            Result = GetResult(evetns.Where(ev => ev.TestId == t.TestId).ToList(), r, t)
                        }).ToList()
                    }
                })
            }).ToList();

            var final_diagnosis = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "991002A" /*Final Diagnosis*/).Select(s => s.ResultAlpha).FirstOrDefault();
            var specimen_adequacy = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "992002A" /*Specimen Adequacy*/).Select(s => s.ResultAlpha).FirstOrDefault();
            var gross_description = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990011A" /*Gross Description*/).Select(s => s.ResultAlpha).FirstOrDefault();
            var prior_history = new PriorHistoryTestsMsSql(_acc_id).Get();

            bool IsOBGYN = true;
            test_result.ForEach(f => { if (!Tests.OBGYN.Contains(f.PanelId)) { IsOBGYN = false; return; } });
            var result = new
            {
                AllTestsIsOBGYN = IsOBGYN,
                AllTestsIsDrugs = lab_result.Any(a => a.PanelType != "D" && a.PanelType != "S") ? 0 : 1,
                HasAllergenTests = lab_result.Any(a => a.IsAllergen == 1) ? 1 : 0,
                HasDrugsTests = lab_result.Any(a => a.PanelType == "D" || a.PanelType == "S") ? 1 : 0,
                HasAbnormalTests = test_result.Any(a => (String.IsNullOrEmpty(a.Flag) || a.Flag == "N" ? 0 : 1) == 1) ? 1 : 0,
                HasSTITests = test_result.Any(a => Tests.STITests.Contains(a.TestId)),
                HasThinPrepPanels = test_result.Any(a => new string[] { "2975", "2975-2" }.Contains(a.PanelId)),
                Order = order_info,
                Panels = panels,
                Drugs = drugs,
                GynCytology = new
                {
                    Diagnosis = !String.IsNullOrEmpty(final_diagnosis) ? final_diagnosis : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "991002A").Select(s => s.PrintNotes).FirstOrDefault(),
                    Adequacy = !String.IsNullOrEmpty(specimen_adequacy) ? specimen_adequacy : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "992002A").Select(s => s.PrintNotes).FirstOrDefault(),
                    Comments = !String.IsNullOrEmpty(gross_description) ? gross_description : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990011A").Select(s => s.PrintNotes).FirstOrDefault(),
                    TestOrdered = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990004A" /*Gross*/).Select(s => s.ResultAlpha).FirstOrDefault(),
                    ClinicalInformation = test_result.Where(w => w.PanelId == "2975" && w.TestId == "993001" /*ClinicalHistory*/).OrderByDescending(o => o.CreatedDate).Select(s => s.ResultAlpha).FirstOrDefault(),
                    PriorHistory = prior_history.Any() ? new PreviousResultsMsSql(order_info.PatId, order_info.CreatedDate, "991002A").Get().Select(r => new
                    {
                        SpecimenNumber = r.AccId,
                        ReceivedDate = r.ReceivedDate,
                        ReportDate = r.PrintedDate,
                        ReportType = r.PanelName,
                        Diagnosis = r.ResultAlpha,
                        Test = prior_history
                    }).ToList() : null
                }
            };

            return result;
        }

        private string GetResult(List<EventData> events, PanelResultData r, OrderResultData od)
        {
            var type = GetResultType(r.Flag);
            string result = r.ResultNumeric == null ? (r.ResultAlpha == "OCC" ? r.ResultTranslation : r.ResultAlpha) : (Convert.ToString(od.DecPlaces == null ? r.ResultNumeric.Value : Math.Round(r.ResultNumeric.Value, od.DecPlaces.Value, MidpointRounding.AwayFromZero)) + (type != null ? " " + type.prefix : ""));

            var result_translation = r.ResultTranslation;

            foreach (var test_event in events)
            {
                var CheckInclusion = test_event.IsFlag;
                var result_type = od.TestResultType.ToLowerInvariant();
                if ("a" == result_type || "m" == result_type)
                {

                    var result_alpha = r.ResultAlpha;
                    var includes_range_text = test_event.AlphaText.Split(',').Any(v => string.Equals(result_alpha.Replace("\"", ""), v)
                                   ||
                                   string.Equals(result_alpha.Replace("\"", "").Replace(" ", ""),
                                       v.Replace(" ", "")));

                    if ((CheckInclusion && includes_range_text) || (!CheckInclusion && !includes_range_text))
                    {
                        if (string.IsNullOrEmpty(result_translation))
                        {
                            result_translation = test_event.ResultTranslation;
                        }
                        else
                        {
                            result_translation += ", " + test_event.ResultTranslation;
                        }
                    }
                }

                if ((r.ResultNumeric >= test_event.GreaterThan && r.ResultNumeric < test_event.LessThan) && !string.IsNullOrEmpty(result_translation))
                {
                    return result_translation;
                }
            }

            return result;
        }

        public string RemoveControlCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var new_string = new StringBuilder();
            foreach (var ch in input.Where(
                ch => !char.IsControl(ch) || '\n' == ch || '\r' == ch || '\t' == ch
                ))
            {
                new_string.Append(ch);
            }

            return new_string.ToString().Trim();
        }

        private OrderInfoView GetOrderInfo(LabdaqClient lab, int acc_id)
        {
            var args = new System.Collections.Generic.Dictionary<string, object> { { "acc_id", acc_id } };
            var SQL_GET_ORDER = @"
                SELECT
                  rq.ACC_ID,
                  rq.PRINTED_DATE,
                  LD1.fnc_req_status_text(rq.ACC_ID) as FINAL_STATUS,
                  rq.draw_date,
                  rq.received_date,
                  rq.NOTES,
                  rq.pat_id,
                  rq.CREATED_DATE
                FROM REQUISITIONS rq 
                INNER JOIN patients pt ON rq.pat_id = pt.pat_id
                INNER JOIN doctors doc ON rq.doc_id1 = doc.doc_id
                WHERE rq.ACC_ID = :acc_id ";

            return lab.RunSql(SQL_GET_ORDER, args).Select(s => new OrderInfoView
            {
                AccId = Convert.ToInt32(s["ACC_ID"]),
                PrintedDate = s["PRINTED_DATE"] != DBNull.Value ? (DateTime?)s["PRINTED_DATE"] : null,
                FinalStatus = s["FINAL_STATUS"].ToString(),
                Notes = s["NOTES"].ToString(),
                PatId = s["PAT_ID"].ToString(),
                CreatedDate = (DateTime)s["CREATED_DATE"]
            }).FirstOrDefault();
        }

        private OrderInfoView GetOrderInfoMsSQL( int acc_id)
        {
            var SQL_GET_ORDER = @"
                SELECT
                  cast (rq.ACC_ID as int ) AccId,
                  (select ( case when PanelCount = FinalCount then 'FINAL COPY' else 'PRELIMINARY' end ) Status  
                   from (
                           SELECT COUNT(*) as PanelCount , sum ((case when PanelStatus = 'FINAL' then 1 else 0 end ) ) as FinalCount 
                           FROM [labdaq].[dbo].[GetPanelsResult] (  rq.ACC_ID )
                        )x 
                  ) as FinalStatus,
                  rq.PRINTED_DATE PrintedDate,
                  rq.NOTES Notes,
                  rq.pat_id PatId,
                  rq.CREATED_DATE CreatedDate
                FROM REQUISITIONS rq 
                INNER JOIN patients pt ON rq.pat_id = pt.pat_id
                INNER JOIN doctors doc ON rq.doc_id1 = doc.doc_id
                WHERE rq.ACC_ID = {0} ";

     //       var proc = new Proc(SQL_GET_ORDER, "labdaq_mssql") { { "acc_id", acc_id } }.All();
            using (var db = new LabdaqEntities())
            {
                var re = db.Database.SqlQuery<OrderInfoView>(SQL_GET_ORDER, acc_id).First();
                return re;
            }

            /*       return proc.Select(s => new OrderInfo
                   {
                       AccId = Convert.ToInt32(s["AccId"]),
                       PrintedDate = s["PrintedDate"] != DBNull.Value ? (DateTime?)s["PRINTED_DATE"] : null,
                       FinalStatus = s["FinalStatus"].ToString(),
                       Notes = s["Notes"].ToString(),
                       PatId = s["PatId"].ToString(),
                       CreatedDate = (DateTime)s["CreatedDate"]
                   }).FirstOrDefault();*/
        }


        private int? GetAllergenLevel(decimal? ResultNumeric)
        {
            if (ResultNumeric < 0.35m)
            {
                return 0;
            }
            else if (ResultNumeric >= 0.35m && ResultNumeric < 0.7m)
            {
                return 1;
            }
            else if (ResultNumeric >= 0.7m && ResultNumeric < 3.5m)
            {
                return 2;
            }
            else if (ResultNumeric >= 3.5m && ResultNumeric < 17.5m)
            {
                return 3;
            }
            else if (ResultNumeric >= 17.5m && ResultNumeric < 50m)
            {
                return 4;
            }
            else if (ResultNumeric >= 50m && ResultNumeric < 100m)
            {
                return 5;
            }
            else if (ResultNumeric >= 100m)
            {
                return 6;
            }
            else
            {
                return null;
            }
        }

        private decimal GetLessByLevel(decimal? ResultNumeric)
        {
            switch (GetAllergenLevel(ResultNumeric))
            {
                case 0:
                    return 0.35m;

                case 1:
                    return 0.7m;

                case 2:
                    return 3.5m;

                case 3:
                    return 17.5m;

                case 4:
                    return 50m;

                case 5:
                    return 100m;

                default:
                    return 100;
            }
        }

        private ResultTypeData GetResultType(string flag)
        {

            switch (flag)
            {
                case "L":
                    return new ResultTypeData
                    {
                        color = "blue",
                        prefix = "LO"
                    };
                case "PL":
                    return new ResultTypeData
                    {
                        color = "blue",
                        prefix = "PL"
                    };
                case "H":
                    return new ResultTypeData
                    {
                        color = "red",
                        prefix = "HI"
                    };
                case "PH":
                    return new ResultTypeData
                    {
                        color = "red",
                        prefix = "PH"
                    };
                case "BL":
                    return new ResultTypeData
                    {
                        color = "grey",
                        prefix = "Borderline Low"
                    };
                case "BH":
                    return new ResultTypeData
                    {
                        color = "grey",
                        prefix = "Borderline High"
                    };
                case "CL":
                    return new ResultTypeData
                    {
                        color = "blue",
                        prefix = "Critical Low"
                    };
                case "CH":
                    return new ResultTypeData
                    {
                        color = "red",
                        prefix = "Critical High"
                    };
                case "+":
                    return new ResultTypeData
                    {
                        color = "red",
                        prefix = "Positive"
                    };
                case "-":
                    return new ResultTypeData
                    {
                        color = "orange",
                        prefix = "Negative"
                    };
                case "A":
                    return new ResultTypeData
                    {
                        color = "red",
                        prefix = "Abnormal"
                    };
                case "N":
                    return new ResultTypeData
                    {
                        color = "black",
                        prefix = "Normal"
                    };
                case "E":
                    return new ResultTypeData
                    {
                        color = "grey",
                        prefix = "Equivocal"
                    };
                default:
                    return null;
            }

        }

        private string InsertReference(OrderResultData reader, List<PanelResultData> results)
        {
            var result_type = reader.TestResultType.ToLowerInvariant();
            string reference;
            if ("a" == result_type || "m" == result_type)
            {
                reference = reader.AlphaRangeText ?? "";
            }
            else
            {
                if (results.Count < 1)
                {
                    reference = "N/A";
                }
                else
                {
                    var result = results[0];

                    string sign = "-";
                    if (result.LowOrMean == null || result.HighOrSd == null)
                    {
                        sign = "<";
                    }

                    reference = (result.LowOrMean == null ? "" : Convert.ToString(result.LowOrMean)) + sign + (result.HighOrSd == null ? "" : Convert.ToString(result.HighOrSd));
                }
            }

            if (reference == "0-0" || reference == "<")
            {
                reference = "";
            }

            if (reference.IndexOf("0-") == 0)
            {
                reference = "<" + reference.Substring(2);
            }

            return reference;
        }

    }


    public static class ExtObject
    {
        public static decimal? ToDecimal(this object s)
        {
            return s != DBNull.Value ? (decimal?)Convert.ToDecimal(s) : null;
        }
        public static int? ToInt32(this object s)
        {
            return s != DBNull.Value ? (int?)Convert.ToInt32(s) : null;
        }
    }
}
