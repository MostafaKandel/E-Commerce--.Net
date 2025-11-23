using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext) {
            var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage).ToArray());
            var Problem = new ProblemDetails()
            {
                Title = "Validation Error",
                Detail = "One or more Validation errors occured",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                        {
                            {"Errors", errors }
                        }
            };
            return new BadRequestObjectResult(Problem);

        }
    }
}
