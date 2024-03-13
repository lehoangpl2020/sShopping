using FluentValidation;

namespace Weather.API.ProblemDetails
{
    public class BadRequestProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BadRequestProblemDetails(ValidationException ex)
        {
            Status = StatusCodes.Status400BadRequest;
            Title = ex.Message;
            Detail = string.Join(";", ex.Errors);
            Type = "https://httpstatuses.com/400";
        }
    }
}
