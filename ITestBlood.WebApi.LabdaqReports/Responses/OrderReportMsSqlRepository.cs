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
    public class OrderReportMsSqlRepository
    {
        private int _acc_id { get; set; }

        public OrderReportMsSqlRepository(int acc_id)
        {
            _acc_id = acc_id;
        }

        public object GetOrderPanelsMsSQL()
        {
            var order_info = new OrderInfoMsSQL(_acc_id).Get();

          

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
                PrintNotes = Helper.RemoveControlCharacters(s["PRINT_NOTES"].ToString()),
                AddedAfterPrint = s["PRINT_NOTES"].ToString() == "T" ? 1 : 0,
                TestResultType = s["RESULT_TYPE"].ToString(),
                DecPlaces = s["DEC_PLACES"].ToInt32(),
                AlphaRangeText = s["ALPHA_RANGE_TEXT1"].ToString(),
                IsAllergen = new Regex(@"\[[A-Z]\d{1,3}\]").IsMatch(s["TEST_NAME"].ToString()) ? 1 : 0,
                StorageStability = s["STORAGE_STABILITY"].ToString(),
            }).ToList().ForEach(item =>
            {
                if (!lab_result.Any(a=> a.TestId == item.TestId  && a.PanelType == item.PanelType) /*Select(s => s.TestId).Contains(item.TestId)*/)
                {
                    lab_result.Add(item);
                }
            });

            var prev_result = new PreviousResultsMsSql(order_info.PatId, order_info.CreatedDate).Get();

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
                PrevResultDates = prev_result.Where(w => w.PanelId == s.Key.PanelId).OrderByDescending(o => o.CreatedDate).Select(ps => ps.CreatedDate).Distinct().Take(6),
                Tests = s.Select(t => new
                {
                    t.TestId,
                    t.TestName,
                    t.PrintNotes,
                    Reference = Helper.InsertReference(t, test_result.Where(w => w.PanelId == s.Key.PanelId && w.TestId == t.TestId).ToList()),
                    STINoteIndex = Tests.STITests.Contains(s.Key.PanelId) && !String.IsNullOrEmpty(t.PrintNotes) ? (int?)++STINoteIndex : null,
                    t.IsAllergen,
                    t.StorageStability,
                    Results = new
                    {
                        Current = test_result.Where(w => w.PanelId == s.Key.PanelId && w.TestId == t.TestId).Select(r => new
                        {
                            r.RpId,
                            r.CreatedDate,
                            r.ResultNumeric,
                            r.Flag,
                            r.Units,
                            CutOff = r.HighOrSd != null ? Math.Round(r.HighOrSd.Value) : 0,
                            r.HighOrSd,
                            r.IsPositive,
                            r.Corrected,
                            AllergenLevel = t.IsAllergen == 1 ? Helper.GetAllergenLevel(r.ResultNumeric) : null,
                            AllergenLevelPercent = t.IsAllergen == 1 ? (int?)Convert.ToInt32((100 * r.ResultNumeric) / Helper.GetLessByLevel(r.ResultNumeric)) : null,
                            IsAbnormalFlag = (String.IsNullOrEmpty(r.Flag) || r.Flag == "N" ? 0 : 1),
                            ResultType = Helper.GetResultType(r.Flag),
                            r.ResultCodeText,
                            r.ResultAlpha,
                            Result = Helper.GetResult(evetns.Where(ev => ev.TestId == t.TestId).ToList(), r, t),
                            Outcome = new string[] { "S", "D" }.Contains(s.Key.PanelType) ? (r.IsPositive == 1 ? "POSITIVE" : "NEGATIVE") : "",
                            IsInconsistentResult = (drugs.Consistent.Any(a => a.TestId == t.TestId) || (!drugs.Inconsistent1.Any(a => a.TestId == t.TestId) && r.ResultNumeric <= r.HighOrSd)) ? 0 : 1
                        }).FirstOrDefault(),
                        Previous = prev_result.Where(w => w.TestId == t.TestId).Select(r => new
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
                            AllergenLevel = t.IsAllergen == 1 ? Helper.GetAllergenLevel(r.ResultNumeric) : null,
                            AllergenLevelPercent = t.IsAllergen == 1 ? (100 * r.ResultNumeric) / Helper.GetLessByLevel(r.ResultNumeric) : null,
                            IsAbnormalFlag = (String.IsNullOrEmpty(r.Flag) || r.Flag == "N" ? 0 : 1),
                            ResultType = Helper.GetResultType(r.Flag),
                            r.ResultCodeText,
                            r.ResultAlpha,
                            Result = Helper.GetResult(evetns.Where(ev => ev.TestId == t.TestId).ToList(), r, t)
                        }).ToList().OrderByDescending(o => o.CreatedDate).Take(6)
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
                    IsAbnormalDiagnosis = panels.Any(a => a.PanelId == "2975-2" && a.Tests.Any(ta => ta.TestName == "Abnormal" && ta.Results.Current != null && ta.Results.Current.ResultAlpha == "True")) ? 1 : 0,
                    Adequacy = !String.IsNullOrEmpty(specimen_adequacy) ? specimen_adequacy : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "992002A").Select(s => s.PrintNotes).FirstOrDefault(),
                    Comments = !String.IsNullOrEmpty(gross_description) ? gross_description : lab_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990011A").Select(s => s.PrintNotes).FirstOrDefault(),
                    TestOrdered = test_result.Where(w => w.PanelId == "2975-2" && w.TestId == "990004A" /*Gross*/).Select(s => s.ResultAlpha).FirstOrDefault(),
                    ClinicalInformation = test_result.Where(w => w.PanelId == "2975" && w.TestId == "993001" /*ClinicalHistory*/).OrderByDescending(o => o.CreatedDate).Select(s => s.ResultAlpha).FirstOrDefault(),
                    PriorHistory = prior_history.Any() ? prev_result.Where(w => w.TestId == "991002A").Select(r => new
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

        const string sql = @"
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

    }


}
