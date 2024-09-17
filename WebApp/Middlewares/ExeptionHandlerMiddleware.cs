using System.Net;
using UseCases.Exeptions;

namespace WebApp.Middlewares
{
    public class ExeptionHandlerMiddleware
    {
        public readonly RequestDelegate _next;
        public ExeptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundExeption ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

    }
}
