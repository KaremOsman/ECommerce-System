using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorsResponse(ActionContext context)
        {
            var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(er => er.ErrorMessage).ToArray()
                        }).ToArray();
            var errorResponse = new
            {
                Message = "Validation Failed",
                Errors = errors
            };
            return new BadRequestObjectResult(errorResponse);
        }
    }
}
