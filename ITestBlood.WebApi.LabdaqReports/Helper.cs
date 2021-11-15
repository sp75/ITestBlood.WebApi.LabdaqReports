using ITestBlood.WebApi.LabdaqReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports
{
    public static class Helper
    {
        public static string GetResult(List<EventData> events, PanelResultData r, OrderResultData od)
        {
            var type = GetResultType(r.Flag);
            string result = r.ResultNumeric == null ? (!string.IsNullOrEmpty(r.ResultTranslation) ? r.ResultTranslation : r.ResultAlpha) : (Convert.ToString(od.DecPlaces == null ? r.ResultNumeric.Value : Math.Round(r.ResultNumeric.Value, od.DecPlaces.Value, MidpointRounding.AwayFromZero)) + (type != null ? " " + type.prefix : ""));

            var result_translation = !string.IsNullOrEmpty(r.ResultTranslation) ? $"{r.ResultTranslation} {(type != null ? type.prefix : "")}" : "";

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

                if (!string.IsNullOrEmpty(result_translation))
                {
                    if (test_event.LessThan != null && test_event.GreaterThan != null)
                    {
                        if ((r.ResultNumeric <= test_event.LessThan) && (r.ResultNumeric >= test_event.GreaterThan))
                        {
                            return result_translation;
                        }
                    }
                    else if (test_event.LessThan != null)
                    {
                        if (r.ResultNumeric < test_event.LessThan)
                        {
                            return result_translation;
                        }
                    }
                    else if (test_event.GreaterThan != null)
                    {
                        if (r.ResultNumeric > test_event.GreaterThan)
                        {
                            return result_translation;
                        }
                    }
                }

              /*  if ((r.ResultNumeric >= test_event.GreaterThan && r.ResultNumeric < test_event.LessThan) && !string.IsNullOrEmpty(result_translation))
                {
                    return result_translation;
                }*/
            }

            return result;
        }
        public static string RemoveControlCharacters(string input)
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
        public static int? GetAllergenLevel(decimal? ResultNumeric)
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
        public static decimal GetLessByLevel(decimal? ResultNumeric)
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
        public static ResultTypeData GetResultType(string flag)
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
        public static string InsertReference(OrderResultData reader, List<PanelResultData> results)
        {
            var result_type = reader.TestResultType.ToLowerInvariant();
            string reference;
            if ("a" == result_type || "m" == result_type || string.IsNullOrEmpty(result_type) )
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