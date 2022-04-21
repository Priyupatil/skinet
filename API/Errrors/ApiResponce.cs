using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errrors
{
    public class ApiResponce
    {
        public ApiResponce(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message{ get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made.",
                401 => "Authorized,you are not",
                404 => "Resource found,It was not",
                500 => "Errors are the path to the dark side",
                _ => null
            };
        }
    }
}