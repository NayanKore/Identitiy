using Identity.Application.Features.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Identity.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An error occurred.";

            switch (exception)
            {
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    break;
                //case ValidationException:
                //    statusCode = HttpStatusCode.BadRequest;
                //    message = exception.Message;
                //    break;
                default:
                    _logger.LogError(exception, "Unhandled exception");
                    break;
            }

            response.StatusCode = (int)statusCode;
            var result = JsonSerializer.Serialize(new { statusCode = (int)statusCode, message });
            await response.WriteAsync(result);
        }
    }
}
