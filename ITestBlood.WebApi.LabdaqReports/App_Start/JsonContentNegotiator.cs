using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace ITestBlood.WebApi.LabdaqReports
{
    /// <summary>
    /// This negotiator insures we have only JSON response type.
    /// </summary>
    public class JsonContentNegotiator : IContentNegotiator
    {
        private readonly MediaTypeFormatter _json_formatter;

        public JsonContentNegotiator( MediaTypeFormatter formatter ) 
        {
            _json_formatter = formatter;    
        }

        public ContentNegotiationResult Negotiate(
            Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters
            )
        {
            var result = new ContentNegotiationResult( 
                _json_formatter, 
                new MediaTypeHeaderValue( "application/json" ) 
                );

            return result;
        }
    }
}