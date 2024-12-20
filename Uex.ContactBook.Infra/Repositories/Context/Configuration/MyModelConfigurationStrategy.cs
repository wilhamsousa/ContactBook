using Microsoft.EntityFrameworkCore;

namespace Uex.ContactBook.Infra.Repositories.Context.Configuration
{
    public static class MyModelConfigurationStrategy
    {
        public static void Configure(ModelBuilder builder, IEntityConfigurationStrategy entityConfiguration)
        {
            entityConfiguration.Configure(builder);
        }
    }
}
