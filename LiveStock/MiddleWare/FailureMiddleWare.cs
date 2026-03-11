using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiveStockBL.MiddleWare
{
    public class FailureMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly Random _random = new();

        public FailureMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_random.Next(1, 101) <= 10)
            {
                context.Response.StatusCode = 500;
                return;
            }

            await _next(context);
        }
    }
}
