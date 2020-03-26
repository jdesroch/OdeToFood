using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Middleware
{
    public class SayHelloMiddleware
    {
        private readonly RequestDelegate next;

        public SayHelloMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/hello"))
            {
                await context.Response.WriteAsync("Hello, World!");
            }
            else
            {
                await next(context);
            }
        }
    }
}
