using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VintageCars.Domain.Exceptions.Response
{
    public class ErrorDetails : Exception
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorDetails(Exception e, int code)
            :base(e.Message)
        {
            StatusCode = code;
            Message = e.Message;
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
