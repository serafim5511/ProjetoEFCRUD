using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using Domain.Helpers;
using Newtonsoft.Json;

namespace API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsnc(context, ex);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsnc(context, exception);
            }
        }
        private Task HandleExceptionAsnc(HttpContext context, HttpStatusCodeException exception)
        {
            Error result;
            context.Response.ContentType = "application/json";
            if (exception is HttpStatusCodeException)
            {
                result = new Error()
                {
                    Code = ((int)exception.StatusCode).ToString(),
                    Title = exception.StatusCode.ToString(),
                    Detail = exception.Message
                };
                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                result = new Error()
                {
                    Code = ((int)HttpStatusCode.InternalServerError).ToString(),
                    Title = "Internal Server Error",
                    Detail = exception.Message
                };
                context.Response.StatusCode = (int)exception.StatusCode;
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = result
            }));
        }
        private Task HandleExceptionAsnc(HttpContext context, Exception exception)
        {
            List<Error> erro = new List<Error>()
            {
                new Error()
                {
                    Code = ((int)HttpStatusCode.InternalServerError).ToString(),
                    Title = "Internal Server Error",
                    Detail = exception.Message
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(erro));
        }
    }
}
