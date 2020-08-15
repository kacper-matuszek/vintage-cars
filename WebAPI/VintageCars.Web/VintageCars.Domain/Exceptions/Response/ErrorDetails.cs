using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VintageCars.Domain.Exceptions.Response
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        #region Ctor
        public ErrorDetails(Exception e, int code)
        {
            StatusCode = code;
            Message = e.Message;
        }

        public ErrorDetails(string message, int code)
        {
            StatusCode = code;
            Message = message;
        }
        #endregion
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
