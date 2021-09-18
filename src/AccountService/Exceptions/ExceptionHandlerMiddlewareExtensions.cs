using System;
using Microsoft.AspNetCore.Builder;

namespace AccountService.Exceptions
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(
            this IApplicationBuilder app
        )
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
