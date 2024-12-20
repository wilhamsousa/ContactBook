using Microsoft.EntityFrameworkCore;

namespace Uex.ContactBook.Infra.Repositories.Context.Configuration
{
    public interface IEntityConfigurationStrategy
    {
        void Configure(ModelBuilder builder);
    }
}
