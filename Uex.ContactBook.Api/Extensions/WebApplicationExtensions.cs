using Uex.ContactBook.Infra.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Uex.ContactBook.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void ApplyMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _Db = scope.ServiceProvider.GetRequiredService<ContactBookContext>();
                if (_Db != null)
                {
                    if (_Db.Database.GetPendingMigrations().Any())
                    {
                        _Db.Database.Migrate();
                    }
                }
            }
        }
    }
}
