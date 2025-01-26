using Pharmacy.Application.Exceptions;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Middleware;

public class ExceptionHandlerMiddleWare(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(Result<object>.Fail([.. ex.Errors]));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //TODO: Log the exception

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(Result<object>.Fail(Messages.SomethingWentWrong));
        }
    }
}

