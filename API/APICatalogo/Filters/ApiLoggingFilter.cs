using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)//Executa depois da Action do Controller
    {
        _logger.LogInformation($"Executando OnActionExecuted ---------> {DateTime.Now.ToLongTimeString()}");
    }

    public void OnActionExecuting(ActionExecutingContext context)//executa antes da Action do Controller
    {
        _logger.LogInformation($"Executando OnActionExecuting ---------> {DateTime.Now.ToLongTimeString()}");
    }
}
