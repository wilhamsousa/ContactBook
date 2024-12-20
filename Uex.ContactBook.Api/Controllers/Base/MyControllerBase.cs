using FluentValidation.Results;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;

namespace Uex.ContactBook.Api.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MyControllerBase: ControllerBase
    {
        private readonly NotificationContext _notificationContext;

        protected MyControllerBase(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        protected ActionResult CreateResult(object responseObject, string errorTitle)
        {
            if (_notificationContext.HasNotifications)
            {
                var errors = _notificationContext.Notifications.Select(x => x.Message);

                var dictionary = new Dictionary<string, object>();
                dictionary["errors"] = errors;

                var problemDetails = new ProblemDetails()
                {
                    Title = errorTitle,
                    Status = ((int)HttpStatusCode.BadRequest),
                    Detail =  errors.Count() == 1 ? errors.FirstOrDefault() : "Multiple erros have occurred.",
                    Instance = Request.Path,
                    Extensions = dictionary
                };

                return BadRequest(problemDetails);
            }
            
            return Ok(responseObject);
        }

        //protected ActionResult CreateResult(IEnumerable<object> responseObject) =>
        //    responseObject == null || !responseObject.Any() ? NotFound("Registro não encontrado") : Ok(responseObject);

        //protected ActionResult CreateResult() => Ok();

        protected void AddValidationFailure(string message)
        {
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure() { ErrorMessage = message });
            _notificationContext.AddNotifications(validationResult);
        }

        protected void AddNotifications(ValidationResult validationResult)
        {
            if (validationResult != null && validationResult.Errors.Any())
                _notificationContext.AddNotifications(validationResult);
        }

        protected bool ValidateRequest(Guid id)
        {
            bool valid = (id == null || id == Guid.Empty);
            if (!valid)
                AddValidationFailure(INVALID_ID);

            return valid;
        }

        public const string
            INVALID_ID = "Id não informado.";
    }
}
