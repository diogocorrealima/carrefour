using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FluxoDeCaixa.Application.ViewModels
{
    public class ErrorResult
    {
        public ErrorResult(List<ErrorItem> errors)
        {
            Errors = errors;
        }

        public List<ErrorItem> Errors { get; set; }

    }
    public class ErrorMessage
    {
        public ErrorMessage(string type, string message)
        {
            Type = type;
            Message = message;
        }

        public string Type { get; set; }
        public string Message { get; set; }
    }
    public class ErrorDetail
    {
        public ErrorDetail(int statusCode, ErrorMessage message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; set; }
        public ErrorMessage Message { get; set; }
    }
    public class ErrorItem
    {
        public ErrorItem(string type, string message)
        {
            Type = type;
            Message = message;
        }

        public string Type { get; set; }
        public string Message { get; set; }

    }

   
}
