namespace CompanyStore.Data.Migrations
{
    using CompanyStore.Entity;
    using System;
    using System.Collections.Generic;
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

            // Create Department
            context.Departments.AddOrUpdate(e => e.Name, MockDataInitializer.GenerateDepartments().ToArray());
            // Create Employees
            context.Employees.AddOrUpdate(e => e.FirstName, MockDataInitializer.GenerateEmployees().ToArray());
            //  Create Genres
            context.Categories.AddOrUpdate(g => g.Name, MockDataInitializer.GenerateCategories().ToArray());
            // Create Devices
            context.Devices.AddOrUpdate(d => d.Name, MockDataInitializer.GenerateDevices().ToArray());
            // Create Stocks
            context.Stocks.AddOrUpdate(MockDataInitializer.GenerateStocks().ToArray());
            // Create Roles
            context.Roles.AddOrUpdate(r => r.Name, MockDataInitializer.GenerateRoles().ToArray());
            // Create Users
            context.Users.AddOrUpdate(u => u.Username, new User[]{
                new User()
                {
                    Email = "u510610433@gmail.com",
                    FirstName = "Wanasak",
                    LastName = "Suraintaranggoon",
                    Username = "smudger",
                    IsLocked = false,
                    CreatedDate = DateTime.Now,
                    HashedPassword = "2O65mFzQWIxmfzbkPjeVnS3c8U0IN07oE8ymQWwgY5Y=",
                    Salt = "ljd/YZrfxnkEoB0l2rvjgA==",
                    Image = "twitter-profile.jpg"
                }
            });
            // Create user admin for smudger
            context.UserRoles.AddOrUpdate(new UserRole[]{
                new UserRole()
                {
                    UserID = 1,
                    RoleID = 1
                }
            });
        }
        
    }
}
