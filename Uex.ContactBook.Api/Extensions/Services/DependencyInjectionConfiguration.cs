using Uex.ContactBook.Application.Services;
using Uex.ContactBook.Domain.Interfaces;
using Uex.ContactBook.Domain.Interfaces.External;
using Uex.ContactBook.Domain.Notification;
using Uex.ContactBook.Infra.Repositories;

namespace Uex.ContactBook.Api.Extensions.Services
{
    public static partial class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();

            services.AddScoped<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddScoped<IUserServiceAsync, UserServiceAsync>();

            services.AddScoped<IContactRepositoryAsync, ContactRepositoryAsync>();
            services.AddScoped<IContactServiceAsync, ContactServiceAsync>();

            services.AddScoped<ILoginServiceAsync, LoginServiceAsync>();
            services.AddScoped<IExternalServiceAsync, ExternalServiceAsync>();
        }
    }
}
