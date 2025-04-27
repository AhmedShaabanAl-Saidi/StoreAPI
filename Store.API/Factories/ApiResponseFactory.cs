using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Store.API.Factories
{
    public class ApiResponseFactory
    {
        public static ActionResult CustomValidtionErrorRespose(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationError
                {
                    Key = error.Key,
                    Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                });

            var validationResponse = new ValidationErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Validation Faild",
                Errors = errors
            };

            return new BadRequestObjectResult(validationResponse);
        }
    }
}
