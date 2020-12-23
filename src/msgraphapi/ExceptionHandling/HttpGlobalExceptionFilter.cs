using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace msgraphapi.ExceptionHandling
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

            if (context.Exception.GetType() == typeof(InvalidRequestException))
            {
                var content = new
                {
                    status = HttpStatusCode.BadRequest,
                    errors = new[]
                    {
                        context.Exception.Message
                    }
                };

                context.Result = new ErrorObjectResult(content);
                context.HttpContext.Response.StatusCode = (int)content.status;
            }

            else if (context.Exception.GetType() == typeof(ServiceException))
            {
                var serviceException = (ServiceException)context.Exception;
                var content = new
                {
                    status = serviceException.StatusCode,
                    errors = new[]
                    {
                        serviceException.Message
                    }
                };

                context.Result = new ErrorObjectResult(content);
                context.HttpContext.Response.StatusCode = (int)content.status;
            }

            else if (context.Exception.GetType() == typeof(AuthenticationException))
            {
                var content = new
                {
                    status = HttpStatusCode.InternalServerError,
                    errors = new[]
                    {
                        $"Failed to authenticate to Azure AD due to a possible internal misconfiguration. Please check whether Azure AD has been configured correctly with the required permissions. {context.Exception.Message}"
                    }
                };

                context.Result = new ErrorObjectResult(content);
                context.HttpContext.Response.StatusCode = (int)content.status;
            }
            else
            {
                var content = new
                {
                    status = HttpStatusCode.InternalServerError,
                    errors = new[]
                    {
                        $"An unexpected error occurred while processing your request. {context.Exception.Message}"
                    }
                };
                context.Result = new ErrorObjectResult(content);
                context.HttpContext.Response.StatusCode = (int)content.status;
            }

            context.ExceptionHandled = true;
        }
    }
}
