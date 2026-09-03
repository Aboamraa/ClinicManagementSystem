using ClinicManagement.Application.Common.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        protected static IActionResult ToActionResult(Result result)
            => result.IsSuccess ? new NoContentResult() : ToProblem(result);



        protected static IActionResult ToActionResult<TResult>(Result<TResult> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Data);
            return ToProblem(result);
        }

        protected static ActionResult ToProblem(Result result)
        {
            // Failure
            var failure = result.Errors[0];

          
            var errorCode = GetStatusCode(failure);

            var problemDetails = new ProblemDetails()
            {
                Title = failure.Title,
                Detail = failure.Description,
                Type = failure.Code,
                Status = errorCode,
            };
            problemDetails.Extensions["errors"] = result.Errors.Select(e => new { e.Title, e.Description, e.Code }).ToList();
            return new ObjectResult(problemDetails) { StatusCode = errorCode };
        }
        private static int GetStatusCode(Error error)
        {
            return error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
