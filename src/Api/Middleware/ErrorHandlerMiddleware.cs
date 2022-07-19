using System.Net;
using Newtonsoft.Json;
namespace Exadel.Forecast.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case ApplicationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case HttpRequestException e:
                        response.StatusCode = (int)e.StatusCode;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonConvert.SerializeObject(new { message = ex.Message });
                
                await response.WriteAsync(result);
            }
        }
    }
}
