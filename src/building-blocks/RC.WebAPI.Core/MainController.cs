using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace RC.WebAPI.Core
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null, HttpStatusCode? successResponseStatusCode = null)
        {
            if (IsValid())
            {
                var responseStatusCode = successResponseStatusCode ?? HttpStatusCode.OK;

                return Ok(new ApiResponse("Success", (int)responseStatusCode, result));
            }

            return BadRequest(new ApiResponse("One or more validation errors ocurred", (int)HttpStatusCode.BadRequest, Errors.ToArray()));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState, HttpStatusCode? successResponseStatusCode = null)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse(successResponseStatusCode: successResponseStatusCode);
        }

        protected ActionResult CustomResponse(ValidationResult validationResult, HttpStatusCode? successResponseStatusCode = null)
        {
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse(successResponseStatusCode: successResponseStatusCode);
        }

        protected bool IsValid()
        {
            return !Errors.Any();
        }

        protected void AddError(string erro)
        {
            Errors.Add(erro);
        }

        protected void ClearErrors()
        {
            Errors.Clear();
        }
    }
}