using ITestBlood.WebApi.LabdaqReports.Models;
using Labdaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.OracleImplementation
{
    public class DrugPanels
    {
        private int _acc_id { get; set; }
        public DrugPanels(int acc_id)
        {
            _acc_id = acc_id;
        }

        public DrugTableData Get()
        {
            using (var lab = new LabdaqClient())
            {
                var args = new Dictionary<string, object> { { "acc_id", _acc_id } };

                // Pull all tests: consistent and inconsistent.
                var all_drugs = lab.RunSql(SQL_ALL_DRUGS_PRESCRIBED, args).Select(s => new DrugConsistent
                {
                    TestId = s["TEST_ID"].ToString(),
                    ReportedPrescription = s["REPORTED_PRESCRIPTION"].ToString(),
                    AnticipatedPositives = s["ANTICIPATED_POSITIVES"].ToString(),
                    DetectionWindow = s["DETECTION_WINDOW"].ToString(),
                    Outcome = s["OUTCOME"].ToString(),
                    Units = s["UNITS"].ToString()
                }).ToList();

                var consistent = all_drugs.Where(drug => drug.Outcome == "POSITIVE").ToList();

                // Tests with negative outcome are inconsistent if all tests from
                // a common panel are negative. If there are positive and negative tests
                // on the same panel--they are all considered consisten.
                var inconsistent1 = new List<DrugInconsistent1>();
                foreach (var drug in all_drugs.Where(drug => drug.Outcome == "NEGATIVE"))
                {
                    bool also_consistent = consistent.Any(consistent_drug => consistent_drug.ReportedPrescription == drug.ReportedPrescription);

                    if (also_consistent)
                    {
                        consistent.Add(drug);
                    }
                    else
                    {
                        inconsistent1.Add(new DrugInconsistent1
                        {
                            TestId = drug.TestId,
                            AnticipatedPositives = drug.AnticipatedPositives,
                            DetectionWindow = drug.DetectionWindow,
                            Outcome = drug.Outcome,
                            ReportedPrescription = drug.ReportedPrescription,
                            Units = drug.Units
                        });
                    }
                }

                var inconsistent2 = lab.RunSql(SQL_INCONSISTENT_TABLE2, args).Select(s => new DrugInconsistent2
                {
                    TestId = s["TEST_ID"].ToString(),
                    DetectedAnalyte = s["DETECTED_ANALYTE"].ToString(),
                    MeasuredResultNumeric = s["MEASURED_RESULT_NUMERIC"].ToString(),
                    Cutoff = s["CUTOFF"].ToString(),
                    DetectionWindow = s["DETECTION_WINDOW"].ToString(),
                    Outcome = s["OUTCOME"].ToString(),
                    Units = s["UNITS"].ToString()
                }).ToList();

                return new DrugTableData
                {
                    Consistent = consistent,
                    Inconsistent1 = inconsistent1,
                    Inconsistent2 = inconsistent2
                };
            }
        }

        const string SQL_INCONSISTENT_TABLE2 = @"
            SELECT tst.test_id,
                tst.test_name as detected_analyte,
                TO_CLOB(case when length(coalesce(tse.RESULT_TRANSLATION, TO_CLOB(''))) > 0 
                  and ( coalesce(tse.LESS_THAN, 0) > rs.RESULT_NUMERIC or coalesce(tse.GREATER_THAN, 0) < rs.RESULT_NUMERIC )
                then
                  tse.RESULT_TRANSLATION
                else
                  TO_CLOB(rs.RESULT_NUMERIC)
                end) as measured_result_numeric,
                round(rs.HIGH_OR_SD) as cutoff,
                'POSITIVE' as outcome,
                tst.STORAGE_STABILITY as detection_window,
                tst.UNITS as units
            FROM REQUISITIONS rq 
            INNER JOIN req_panels rp ON rq.acc_id = rp.acc_id 
            INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
            INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
            INNER JOIN tests tst ON pnt.test_id = tst.test_id 
            LEFT OUTER JOIN test_events tse on tse.test_id = tst.test_id and length(coalesce(tse.RESULT_TRANSLATION, TO_CLOB(''))) > 0 
            INNER JOIN RESULTS rs ON rs.RP_ID = rp.rp_id and rs.DEL_FLAG <> 'T' and rs.TEST_ID = tst.test_id 
            WHERE rq.ACC_ID=:acc_id AND pnt.DISPLAY_ON_REPORT ='T' and rp.DEL_FLAG='F'
                and pnl.panel_type = 'S'
                and rs.RESULT_NUMERIC > rs.HIGH_OR_SD and tst.test_name not in (
                  SELECT  
                      tst.test_name
                  FROM REQUISITIONS rq 
                  INNER JOIN req_panels rp ON rq.acc_id = rp.acc_id 
                  INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
                  INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
                  INNER JOIN tests tst ON pnt.test_id = tst.test_id 
                  WHERE rq.ACC_ID=:acc_id AND pnt.DISPLAY_ON_REPORT ='T' and rp.DEL_FLAG='F' and pnl.panel_type = 'D'
                )
            ORDER BY  
              tst.test_name ASC";

        const string SQL_ALL_DRUGS_PRESCRIBED = @"
            SELECT 
                tst.test_id,
                rx.panel_name as reported_prescription,
                tst.test_name as anticipated_positives,
                (case when rs.RESULT_NUMERIC > rs.HIGH_OR_SD then 'POSITIVE' else 'NEGATIVE' end) as outcome,
                tst.STORAGE_STABILITY as detection_window,
                tst.UNITS as units
            FROM REQUISITIONS rq 
            INNER JOIN req_panels rp ON rq.acc_id = rp.acc_id 
            INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
            INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
            INNER JOIN tests tst ON pnt.test_id = tst.test_id 
            INNER JOIN RESULTS rs ON rs.RP_ID = rp.rp_id and rs.DEL_FLAG <> 'T' and rs.TEST_ID = tst.test_id
            INNER JOIN (SELECT  
                  tst.test_name,
                  pnl.panel_name
              FROM REQUISITIONS rq 
              INNER JOIN req_panels rp ON rq.acc_id = rp.acc_id 
              INNER JOIN panels pnl ON rp.panel_id = pnl.panel_id 
              INNER JOIN panel_tests pnt ON rp.panel_id = pnt.panel_id 
              INNER JOIN tests tst ON pnt.test_id = tst.test_id 
              WHERE 
                rq.ACC_ID = :acc_id AND 
                pnt.DISPLAY_ON_REPORT ='T' and 
                rp.DEL_FLAG='F' and pnl.panel_type = 'D') rx on rx.test_name = tst.test_name
            WHERE rq.ACC_ID=:acc_id
            AND
                pnt.DISPLAY_ON_REPORT ='T' 
                and rp.DEL_FLAG='F'
                and pnl.panel_type = 'S'
            ORDER BY  
              rx.panel_name, tst.test_name ASC";

    }
}