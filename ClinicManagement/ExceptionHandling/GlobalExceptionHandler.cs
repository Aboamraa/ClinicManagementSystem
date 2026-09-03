using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.ExceptionHandling
{
    public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled Exception");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;


            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "Unexpected Error ocuared please try again later",
                    Type = "Server.Error"

                },
                cancellationToken
            );


            return true;
        }
    }
}
