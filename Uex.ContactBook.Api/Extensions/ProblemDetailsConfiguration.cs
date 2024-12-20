using Hellang.Middleware.ProblemDetails.Mvc;

namespace Uex.ContactBook.Api.Extensions
{
    public static class ProblemDetailsConfiguration
    {
        public static void AddProblemDetailsConfiguration(this IServiceCollection services)
        {
            Hellang.Middleware.ProblemDetails.ProblemDetailsExtensions
                .AddProblemDetails(services, 
                    options => 
                    {
                        options.IncludeExceptionDetails = (ctx, ex) =>
                        {
                            var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                            return env.IsDevelopment() || env.IsStaging();
                        };

                        options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                        options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
                    })
                .AddProblemDetailsConventions();
        }
    }
}
