using System;
using System.IO;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Newtonsoft.Json;

namespace testGQL.middleware
{
    public interface IMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
    
    
    
    
    public class GqlMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;
        private readonly ISchema _schema;

        public GqlMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor, ISchema schema)
        {
            this._next     = next;
            this._writer   = writer;
            this._executor = executor;
            this._schema   = schema;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /* your middleware body */
            if (
                context.Request.Path.StartsWithSegments("/api/graphql") 
                && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase)
               )
            {
                string body;
                using ( var streamReader = new StreamReader(context.Request.Body))
                {
                    body = await streamReader.ReadToEndAsync();
                    
                    var request = JsonConvert.DeserializeObject<GqlRequest>(body);
                    
                    var schema = new Schema { Query = new HelloWorldQuery() };
                    var result = await _executor.ExecuteAsync((ExecutionOptions execOptions) =>
                    {
                        execOptions.Schema = schema;
                        execOptions.Query = request.Query;
                    }).ConfigureAwait(false);    
                    
                    /*Finally, the result of the execution is converted to JSON
                     using the Wrtie() function of the DocumentWriter class. 
                     Last but not least we print out JSON to the response. */
                    var json = _writer.Write(result);
                    await context.Response.WriteAsync(json);
                }

            }
            else
            {
                /* proceed to next middleware */ await _next(context);
            }
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