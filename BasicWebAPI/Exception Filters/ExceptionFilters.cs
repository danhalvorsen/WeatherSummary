using System.Net.Http;
using System;

namespace BasicWebAPI.ExceptionFilters
{
    public class HttpNotFoundException : Exception
    {
        public HttpResponseMessage GetResponseMessage(DateTime date)
        {
            var response = $"Date: {date} is not found.";
            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, response);
            return null;
        }
    }
}
