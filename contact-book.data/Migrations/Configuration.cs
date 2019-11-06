using System.Collections.Generic;

namespace contact_book.data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<contact_book.data.ContactBookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(contact_book.data.ContactBookDbContext context)
        {
            //var types = new List<core.Models.Type>
            //{
            //    new core.Models.Type{ TypeName = "Home"},
            //    new core.Models.Type{ TypeName = "Work"},
            //    new core.Models.Type{ TypeName = "Mobile"},
            //    new core.Models.Type{ TypeName = "Other"}
            //};

            //types.ForEach(type => context.Type.AddOrUpdate(t => t.TypeName, type));

            if (!context.Type.Any())
            {
                var types = new List<core.Models.Type>
                {
                    new core.Models.Type{ TypeName = "Home"},
                    new core.Models.Type{ TypeName = "Work"},
                    new core.Models.Type{ TypeName = "Mobile"},
                    new core.Models.Type{ TypeName = "Other"}
                };

                types.ForEach(type => context.Type.Add(type));
                context.SaveChanges();
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
