using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using VoltMeter.Shared;

namespace VoltMeter.WebAPI.Middlewares;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResult = new BaseResult<string>((int)HttpStatusCode.InternalServerError, exception.Message);

        await httpContext.Response.WriteAsJsonAsync(errorResult, cancellationToken);

        return true;
    }
}