using Uex.ContactBook.Application.Services;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Infra.Repositories;
using Hellang.Middleware.ProblemDetails;

namespace Uex.ContactBook.Api.Extensions
{
    public static partial class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();

            services.AddScoped<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddScoped<IUserServiceAsync, UserServiceAsync>();
        }
    }
}
