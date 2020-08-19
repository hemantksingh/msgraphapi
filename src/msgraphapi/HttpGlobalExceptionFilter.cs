using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace msgraphapi
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception, context.Exception.Message);

            if (context.Exception.GetType() == typeof(ConfigurationException))
            {
                var content = new
                {
                    status = HttpStatusCode.InternalServerError,
                    error = context.Exception.Message
                };
                    
                context.Result = new ErrorObjectResult(content);
                context.HttpContext.Response.StatusCode = (int)content.status;
            }
            else
            {
                var content = new
                {
                    status = HttpStatusCode.InternalServerError,
                    error = $"An unexpected error occurred while processing the request. {context.Exception.Message}"
                };
                context.Result = new ErrorObjectResult(content);
                context.HttpContext.Response.StatusCode = (int)content.status;
            }
        }
    }

    class ErrorObjectResult : ObjectResult
    {
        public ErrorObjectResult(dynamic content) : base((object)content)
        {
            StatusCode = (int?) content.status;
        }
    }
}
