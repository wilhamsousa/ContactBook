using Uex.ContactBook.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Uex.ContactBook.Infra.Repositories.Context.Configuration.Entities
{
    public class UserConfiguration : IEntityConfigurationStrategy
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>()
                .Property(b => b.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<User>()
                .Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
