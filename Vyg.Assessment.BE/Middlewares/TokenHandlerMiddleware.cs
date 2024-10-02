using Vyg.Assessment.BE.Utility;

namespace Vyg.Assessment.BE.Middlewares
{
    public class TokenHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token) && JwtTokenHandler.IsBlacklisted(token))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Token is blacklisted.");
            }

            await _next(context);
        }
    }
}