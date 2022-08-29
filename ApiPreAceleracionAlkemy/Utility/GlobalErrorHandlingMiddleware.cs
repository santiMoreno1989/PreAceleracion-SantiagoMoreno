using ApiPreAceleracionAlkemy.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using UnauthorizedAccessException = ApiPreAceleracionAlkemy.Exceptions.UnauthorizedAccessException;
using NotImplementedException = ApiPreAceleracionAlkemy.Exceptions.NotImplementedException;
using KeyNotFoundException = ApiPreAceleracionAlkemy.Exceptions.KeyNotFoundException;
using System.Text.Json;
using Newtonsoft.Json;
using Serilog;

namespace ApiPreAceleracionAlkemy.Utility
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            
            string message;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.BadRequest;
               
            }
            else if (exceptionType.Equals(typeof(NotFoundException)))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.NotFound;
               
            }
            else if (exceptionType.Equals(typeof(NotImplementedException)))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType.Equals(typeof(UnauthorizedAccessException)))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType.Equals(typeof(KeyNotFoundException)))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.Unauthorized;
   
            }
            else
            {

                statusCode = HttpStatusCode.InternalServerError;
                message = exception.Message;
    
            }

            var exceptionResult = System.Text.Json.JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                Message= message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
