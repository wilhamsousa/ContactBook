
using Microsoft.EntityFrameworkCore;
using Uex.ContactBook.Infra.Repositories.Context;

namespace Uex.ContactBook.Worker.Extensions.Services
{
    public static partial class ContextConfiguration
    {
        public static void AddContextConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ContactBookContext>(
            options => options
                .UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        }
    }
}