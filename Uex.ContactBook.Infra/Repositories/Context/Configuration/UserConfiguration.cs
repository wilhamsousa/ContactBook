using Uex.ContactBook.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Uex.ContactBook.Infra.Repositories.Context.Configuration
{
    public class UserConfiguration : IEntityConfigurationStrategy
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}
