using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ErrorHandler;
using System.Net;
using Newtonsoft.Json;

namespace WebApplication1.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
             }
            catch (Exception ex)
            {
                await AsyncExceptionHandler(context, ex, _logger);
            }
        }

        private async Task AsyncExceptionHandler(HttpContext context, Exception ex, ILogger<ErrorHandlerMiddleware> logger)
        {
            object errores = null;
            switch (ex)
            {
                case ExceptionHandler me:
                    logger.LogError(ex, "Manejador Error");
                    errores = me.Errors;
                    context.Response.StatusCode = (int)me.Code;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }

        }
    }
}
