using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string ApiKeyName="api_key";
    private string ApiKey = Environment.GetEnvironmentVariable("API_KEY");

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(!context.HttpContext.Request.Query.TryGetValue(ApiKeyName, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey não encontrada"
            };

            return;
        }

        if(!ApiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Acesso não autorizado"
            };
            return;
        }
        await next();
    }
}