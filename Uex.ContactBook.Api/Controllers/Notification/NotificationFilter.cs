using Uex.ContactBook.Domain.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Uex.ContactBook.Api.Controllers.Notification
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            const string contentTypeProblem = "application/problem+json";

            if (_notificationContext.HasNotifications && !(context.Result is BadRequestObjectResult))
            {
                var problemDetails = new ProblemDetails()
                {
                    Title = "Erro geral",
                    Status = ((int)HttpStatusCode.BadRequest),
                    Detail = string.Join(",", _notificationContext.Notifications.Select(x => x.Message))
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = contentTypeProblem;

                var problemDetailsJson = JsonConvert.SerializeObject(problemDetails);
                await context.HttpContext.Response.WriteAsync(problemDetailsJson);

                return;
            }

            await next();
        }
    }
}
