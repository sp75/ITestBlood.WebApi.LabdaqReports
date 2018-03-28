using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITestBlood.WebApi.LabdaqReports.Settings
{
    public static class Tests
    {
        public static string[] STITests
        {
            get
            {
                return new string[] { "5005", "5007", "6440", "5519", "5522", "5115", "5117", "5122", "5124", "5120", "5119", "6555", "6545", "6550", "6770", "1905" };
            }
        }

        public static string[] OBGYN
        {
            get
            {
                return new string[] { "5005", "5007", "6440", "5519", "5522", "5115", "5117", "5122", "5124", "5120", "5119", "6555", "6545", "6550", "6770", "1905", "2975", "2975-2" };
            }
        }

    }
}