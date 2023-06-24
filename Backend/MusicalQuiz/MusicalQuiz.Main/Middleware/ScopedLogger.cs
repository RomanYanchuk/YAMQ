using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MusicalQuiz.Main.Middleware
{
    public class ScopedLogger
    {
        private readonly RequestDelegate _next;

        public ScopedLogger(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var logger = context.RequestServices.GetService<ILogger<ScopedLogger>>();
            using (logger?.BeginScope(new[] { new KeyValuePair<string, object>("traceid", context.TraceIdentifier) }))
            {
                await _next.Invoke(context);
            }
        }
    }
}