namespace AdrexamAPI.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TokenService tokenService)
        {
            var apiKey = context.Request.Headers["x-api-key"];
            var uniqueKeys = apiKey.GroupBy(i => i).Select(i => i.Key).ToList();
            if (uniqueKeys.Count == 1 && await tokenService.ValidateAsync(uniqueKeys[0], default))
            {
                await _next.Invoke(context);
            }
            else
            {
                var errorMessage = uniqueKeys.Count == 0 ? Messages.ApiKeyNotFound :
                    uniqueKeys.Count > 1 ? Messages.MustBeOnlyOneApiKeyHeader :
                    Messages.InvalidApiKey;

                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(errorMessage);
            }
        }
    }
}