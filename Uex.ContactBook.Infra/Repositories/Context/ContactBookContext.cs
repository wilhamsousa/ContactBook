using Uex.ContactBook.Domain.Model.Entities;
using Uex.ContactBook.Infra.Repositories.Context.Configuration;
using Microsoft.EntityFrameworkCore;
using Uex.ContactBook.Infra.Repositories.Context.Configuration.Entities;

namespace Uex.ContactBook.Infra.Repositories.Context
{
    public partial class ContactBookContext : DbContext
    {
        public ContactBookContext(DbContextOptions<ContactBookContext> options) : base(options)
        { 
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MyModelConfigurationStrategy.Configure(builder, new UserConfiguration());
        }
    }
}
