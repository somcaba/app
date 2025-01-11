using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Yacaba.Core.Api.Filters {
    public class FluentValidationExceptionFilter : IActionFilter, IOrderedFilter {

        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public Int32 Order => Int32.MaxValue - 10;

        public FluentValidationExceptionFilter(ProblemDetailsFactory problemDetailsFactory) {
            _problemDetailsFactory = problemDetailsFactory;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
        public void OnActionExecuted(ActionExecutedContext context) {
            if (context.Exception is ValidationException validationException) {
                var modelState = new ModelStateDictionary();
                foreach (ValidationFailure error in validationException.Errors) {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                };
                ValidationProblemDetails problemDetails = _problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, modelState, statusCode: StatusCodes.Status400BadRequest, title: "API Validation Failed");
                context.Result = new ObjectResult(problemDetails) {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
