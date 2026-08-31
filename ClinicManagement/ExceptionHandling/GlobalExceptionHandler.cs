using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.ExceptionHandling
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled Exception");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;


            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Unexpected Error ocuared",
                    Detail = "Unexpected Error ocuared please try again later"
                },
                cancellationToken
            );


            return true;
        }
    }
}
