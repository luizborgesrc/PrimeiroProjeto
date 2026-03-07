using System.IO.Pipes;
using System.Net;
using System.Text.Json;

namespace WebAplicationPessoa.WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
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

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Ocorreu um erro processamento da requisição!",
            Detalhes = exception.Message,
        }));

    }
}