namespace SenpaiMarketplace.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SenpaiMarketplace.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SenpaiMarketplace.Models.SenpaiDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SenpaiMarketplace.Models.SenpaiDatabase context)
        {
            //if(!context.Database.Exists())
            //{
            //    context.Database.Create();
            //}
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
