using Microsoft.AspNetCore.Mvc;

namespace msgraphapi.ExceptionHandling
{
    public class ErrorObjectResult : ObjectResult
    {
        public ErrorObjectResult(dynamic content) : base((object)content)
        {
            StatusCode = (int?) content.status;
        }
    }
}