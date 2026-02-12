using FluentValidation;
using System.Text.Json;

namespace APITaskTracker.API.GlobalErrorHandler
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await WriteValidationErrorAsync(context, ex);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found");

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await WriteErrorAsync(context, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await WriteErrorAsync(context, "An unexpected error occurred");
            }
        }

        private static async Task WriteErrorAsync(
            HttpContext context,
            string message)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse(
                Message: message,
                TraceId: context.TraceIdentifier
            );

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }

        private static async Task WriteValidationErrorAsync(
            HttpContext context,
            ValidationException ex)
        {
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var response = new ErrorResponse(
                Message: "Validation failed",
                TraceId: context.TraceIdentifier,
                Errors: errors
            );

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}