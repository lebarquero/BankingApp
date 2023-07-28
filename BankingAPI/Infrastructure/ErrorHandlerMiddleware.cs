using System.Net;
using System.Text.Json;
using BankingAPI.Business.Exceptions;

namespace BankingAPI.Infrastructure
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case BankingAppException:
                        // handle application errors
                        response.StatusCode = (int)HttpStatusCode.BadRequest; 
                        break;
                    case KeyNotFoundException:
                        // handle not found errors
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // log and handle unkown errors
                        _logger.LogError(ex, ex.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
