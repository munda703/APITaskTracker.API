namespace APITaskTracker.API.GlobalErrorHandler
{
    public record ErrorResponse(
        string Message,
        string TraceId,
        IDictionary<string, string[]>? Errors = null
    );
}