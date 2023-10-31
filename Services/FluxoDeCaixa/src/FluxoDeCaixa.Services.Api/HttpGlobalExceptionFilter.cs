using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FluxoDeCaixa.Application.ViewModels;

namespace FluxoDeCaixa.Services.Api
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(
            IWebHostEnvironment webHostEnvironment,
            ILogger<HttpGlobalExceptionFilter> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Result.ToString());

        }

        
    }
}
