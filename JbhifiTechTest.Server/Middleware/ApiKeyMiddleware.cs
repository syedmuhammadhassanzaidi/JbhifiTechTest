namespace JbhifiTechTest.Server.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string apiKeyHeader;
        private readonly List<string> _apiKeys;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            apiKeyHeader = configuration.GetValue<string>("ApiKeyHeader");
            _apiKeys = configuration.GetSection("ApiKeys").Get<List<string>>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(apiKeyHeader, out var apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("There is no API key provided");
                return;
            }
            if (!_apiKeys.Contains(apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("The API Key is invalid");
                return;
            }

            await _next(context);
        }
    }

    public static class ApiKeyVerificationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyVerificaion(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
