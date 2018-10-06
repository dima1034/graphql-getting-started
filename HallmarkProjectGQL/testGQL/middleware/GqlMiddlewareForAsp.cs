using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace testGQL.middleware.forAsp
{
    public interface IMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
    
    
    
    
    public class GqlMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;

        public GqlMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /* your middleware body */
            
            /* proceed to next middleware */ await _next(context);
        }
    }

    
    
    
    public static class GqlExtension
    {
        public static IApplicationBuilder UseGql(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GqlMiddleware>();
        }
    }
}