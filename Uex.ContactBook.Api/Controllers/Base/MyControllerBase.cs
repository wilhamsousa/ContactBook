using FluentValidation.Results;
using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Uex.ContactBook.Domain.Interfaces;

namespace Uex.ContactBook.Api.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MyControllerBase: Controller
    {
        private readonly NotificationContext _notificationContext;
        protected AuthenticationByToken Authentication;

        protected MyControllerBase(
            NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);

            Authentication = GetAuthentication();

            if (Authentication != null && Authentication.UserId != Guid.Empty)
            {
                //if (IsCancelledToken(Authentication.UserId))
                //{
                //    ctx.Result = Unauthorized();
                //    return;
                //}
            }
        }

        protected AuthenticationByToken GetAuthentication()
        {
            AuthenticationByToken authenticationVO = new AuthenticationByToken();
            var accessToken = HttpContext.Request.Headers["Authorization"];

            var claims = User.Claims.ToList();
            if (claims.Count() > 0)
            {
                var claimsIdentity = User.Identities.FirstOrDefault();

                var userId = Guid.Parse(claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).SingleOrDefault());
                var emailClaim = claims.Where(x => x.Type == ClaimTypes.Email).SingleOrDefault();
                string email = emailClaim != null ? emailClaim.Value : null;

                authenticationVO = new AuthenticationByToken
                {
                    AccessToken = accessToken,
                    UserId = userId,
                    UserEmail = email
                };
            }

            return authenticationVO;
        }

        protected ActionResult CreateResult(object responseObject, string errorTitle = null)
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

        public bool HasNotifications => _notificationContext.HasNotifications;
    }
}
