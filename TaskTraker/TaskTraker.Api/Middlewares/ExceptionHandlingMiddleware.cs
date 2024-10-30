using System.Net;
using System.Text.Json;
using TaskTraker.Services.Exceptions;

namespace TaskTraker.Api.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationBaseException appException)
            {
                context.Response.StatusCode = (int)appException.Status;
                var response = new { message = appException.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new { message = "Something went wrong" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
