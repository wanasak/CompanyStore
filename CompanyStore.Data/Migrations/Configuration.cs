namespace CompanyStore.Data.Migrations
{
    using CompanyStore.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CompanyStore.Data.CompanyStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CompanyStore.Data.CompanyStoreContext context)
        {
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

            //  create genres
            context.Categories.AddOrUpdate(g => g.Name, GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            Category[] categories = new Category[] {
                new Category() { Name = "Tablet" },
                new Category() { Name = "Laptop" },
                new Category() { Name = "Desktop" },
                new Category() { Name = "Mobile" },
                new Category() { Name = "Screen" },
                new Category() { Name = "Power Supply" },
                new Category() { Name = "Credit Card Devices" },
                new Category() { Name = "Other" },
            };

            return categories;
        }
    }
}
