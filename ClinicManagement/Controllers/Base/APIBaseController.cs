using ClinicManagement.Application.Common.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        protected static ActionResult ToActionResult(Result result)
            => result.IsSuccess ? new OkObjectResult(result) : ToProblem(result);



        protected static ActionResult<TResult> ToActionResult<TResult>(Result<TResult> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Data);
            else
                return ToProblem(result);

        }

        protected static ActionResult ToProblem(Result result)
        {
            // Failure
            var failure = result.Errors[0];

            var errorCode = failure.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

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
    }
}
