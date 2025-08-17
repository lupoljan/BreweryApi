using System.Net;
using System.Text.Json;

namespace BreweryApi.Utilities
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
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
            catch (ApiException apiEx)
            {
                _logger.LogWarning(apiEx, "Handled API Exception");
                await HandleExceptionAsync(context, apiEx.StatusCode, apiEx.Message, ErrorCodes.ValidationFailed);
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx, "Resource Not Found");
                await HandleExceptionAsync(context, (int)HttpStatusCode.NotFound, knfEx.Message, ErrorCodes.NotFound);
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "External API Failure");
                await HandleExceptionAsync(context, (int)HttpStatusCode.BadGateway, "External service error.", ErrorCodes.ExternalServiceError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error");
                await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, "Internal server error.", ErrorCodes.UnexpectedError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message, string errorCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var error = new ErrorResponse
            {
                Code = errorCode,
                Message = message,
                Detail = context.Request.Path
            };

            var result = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(result);
        }
    }
}
