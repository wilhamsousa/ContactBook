﻿using Microsoft.EntityFrameworkCore;
using Uex.ContactBook.Domain.Model.Entities;

namespace Uex.ContactBook.Infra.Repositories.Context.Configuration.Entities
{
    public class ContactConfiguration : IEntityConfigurationStrategy
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<Contact>()
                .HasIndex(u => new { u.UserId, u.Cpf })
                .IsUnique();

            builder.Entity<Contact>()
                .HasOne(x => x.User)
                .WithMany(x => x.ContactList)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Contact>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Contact>()
                .Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Contact>()
                .Property(b => b.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Entity<Contact>()
                .Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<Contact>()
                .Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<Contact>()
                .Property(b => b.Complement)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Entity<Contact>()
                .Property(b => b.City)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<Contact>()
                .Property(b => b.Uf)
                .IsRequired()
                .HasMaxLength(2);

            builder.Entity<Contact>()
                .Property(b => b.Cep)
                .IsRequired()
                .HasMaxLength(9);

            builder.Entity<Contact>()
                .Property(b => b.GeographicalPosition)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
