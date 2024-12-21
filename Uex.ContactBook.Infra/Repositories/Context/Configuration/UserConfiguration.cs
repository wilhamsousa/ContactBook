using Uex.ContactBook.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Uex.ContactBook.Infra.Repositories.Context.Configuration
{
    public class UserConfiguration : IEntityConfigurationStrategy
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<User>()
                .Property(b => b.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<User>()
                .Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<User>()
                .Property(b => b.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Entity<User>()
                .Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<User>()
                .Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<User>()
                .Property(b => b.Cep)
                .IsRequired()
                .HasMaxLength(9);

            builder.Entity<User>()
                .Property(b => b.GeographicalPosition)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
