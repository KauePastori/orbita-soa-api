using System.Net;
using System.Text.Json;
using Orbita.SoaApi.Application.Common;
using Orbita.SoaApi.Domain.Exceptions;

namespace Orbita.SoaApi.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado.");

            var status = HttpStatusCode.InternalServerError;
            var message = "Ocorreu um erro inesperado.";

            switch (ex)
            {
                case ValidationException ve:
                    status = HttpStatusCode.BadRequest;
                    message = ve.Message;
                    break;
                case NotFoundException nfe:
                    status = HttpStatusCode.NotFound;
                    message = nfe.Message;
                    break;
                case UnauthorizedException ue:
                    status = HttpStatusCode.Unauthorized;
                    message = ue.Message;
                    break;
                case ForbiddenException fe:
                    status = HttpStatusCode.Forbidden;
                    message = fe.Message;
                    break;
                case ConflictException ce:
                    status = HttpStatusCode.Conflict;
                    message = ce.Message;
                    break;
            }

            var resp = ApiResponse<object>.Fail(message);
            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(resp, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(json);
        }
    }
}
