using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://itestblood.lencolab.com" + "/api/labdaq/submit-order";
       //     try
       //     {
                using (var wc = new WebClient())
                {
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var json = JsonConvert.SerializeObject(new { OrderNumber = "1x1x1x1x1x1x", OrderData = "Test" });
                    var response = wc.UploadString(url, json);

        //            return Convert.ToBoolean(response);
                }
       //     }
      //      catch { }

          //  return false;
        }
    }
}
