using System.Net;
using FluentValidation;
using JobsApi.Exceptions;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Middlewares;

[Middleware]
public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await WriteResponse(context, HttpStatusCode.BadRequest, ex.Errors);
        }
        catch (AuthException ex)
        {
            await WriteResponse(context, HttpStatusCode.Unauthorized, "Email or password is not match");
        }
        catch (NotFoundException ex)
        {
            await WriteResponse(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (DuplicateException ex)
        {
            await WriteResponse(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("Message: {}\nStack: {}", ex.Message, ex.StackTrace);
            await WriteResponse(context, HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    private static async ValueTask WriteResponse(HttpContext context, HttpStatusCode code, object? value)
    {
        context.Response.StatusCode = (int)code;

        if (value is not null)
        {
            if (value is string)
            {
                value = new
                {
                    Message = value
                };
            }

            await context.Response.WriteAsJsonAsync(value);
        }
    }
}