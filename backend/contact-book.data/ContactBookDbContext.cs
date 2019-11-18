using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using contact_book.core.Models;
using contact_book.data.Migrations;

namespace contact_book.data
{
    public class ContactBookDbContext : DbContext
    {
        public ContactBookDbContext() : base("contact-book")
        {
            Database.SetInitializer<ContactBookDbContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContactBookDbContext, Configuration>());
        }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
        public DbSet<Type> Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Contact>().Property(m => m.BirthDate).IsOptional();
            base.OnModelCreating(modelBuilder);
        }

        public void SeedMethod()
        {
            if (Type.Any()) return;
            var types = new List<Type>
            {
                new Type{ TypeName = "Home"},
                new Type{ TypeName = "Work"},
                new Type{ TypeName = "Mobile"},
                new Type{ TypeName = "Other"}
            };
            types.ForEach(type => Type.Add(type));
            SaveChanges();
        }
    }
}