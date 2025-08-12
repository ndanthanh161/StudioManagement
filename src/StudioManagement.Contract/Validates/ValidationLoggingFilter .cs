using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class ValidationLoggingFilter : IActionFilter
{
    private readonly ILogger<ValidationLoggingFilter> _logger;

    public ValidationLoggingFilter(ILogger<ValidationLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(ms => ms.Value?.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Messages = ms.Value!.Errors
                        .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                     ? e.Exception?.Message ?? "Invalid"
                                     : e.ErrorMessage)
                        .ToList()
                })
                .ToList();

            foreach (var error in errors)
                foreach (var msg in error.Messages)
                    _logger.LogWarning("Validation error on '{Field}': {Message}", error.Field, msg);

            // Trả về 400 với payload gọn (hoặc dùng ValidationProblemDetails nếu muốn chuẩn)
            context.Result = new BadRequestObjectResult(new { Errors = errors });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
