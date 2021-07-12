using Api.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class ManejadorErroresMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ManejadorErroresMiddleware> _Logger;

        public ManejadorErroresMiddleware(RequestDelegate next, ILogger<ManejadorErroresMiddleware> logger)
        {
            _Next = next;
            _Logger = logger;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _Next(context);
            }
            catch (Exception ex)
            {
                await ManejadorExcepcionAsincrono(context, ex, _Logger);
            }
        }

        private async Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErroresMiddleware> logger)
        {
            object errores = null;
            switch (ex)
            {
                case Error e:
                    logger.LogError(ex, "Manejador Error");
                    errores = e.Errores;
                    context.Response.StatusCode = (int)e.Code;
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
                var respuesta = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(respuesta);
            }

        }
    }
}
