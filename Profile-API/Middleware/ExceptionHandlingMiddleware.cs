using Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Profile_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var message = "Internal Server Error";

            if (exception is BadRequestException)
            {
                statusCode = HttpStatusCode.BadRequest;

                message = exception.Message;
            }
            else if (exception is EntityNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;

                message = exception.Message;
            }

            var result = JsonConvert.SerializeObject(new { error = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
