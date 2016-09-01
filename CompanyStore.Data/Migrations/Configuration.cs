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
        private DateTime _dateFrom = DateTime.Now.AddYears(-15);
        private DateTime _dateTo = DateTime.Now;

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
            context.Departments.AddOrUpdate(e => e.Name, MockDataInitializer.GenerateDepartments());
            // Create Employees
            context.Employees.AddOrUpdate(e => e.FirstName, GenerateEmployees());
            //  Create Genres
            context.Categories.AddOrUpdate(g => g.Name, MockDataInitializer.GenerateCategories());
            // Create Devices
            context.Devices.AddOrUpdate(d => d.Name, MockDataInitializer.GenerateDevices());
            // Create Stocks
            context.Stocks.AddOrUpdate(GenerateStocks());
            // Create Roles
            context.Roles.AddOrUpdate(r => r.Name, GenerateRoles());
            // Create Users
            context.Users.AddOrUpdate(u => u.Username, new User[]{
                new User()
                {
                    Email = "u510610433@gmail.com",
                    FirstName = "Wanasak",
                    LastName = "Suraintaranggoon",
                    Username = "smudger",
                    IsLocked = false,
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
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

        
        private Role[] GenerateRoles()
        {
            return new Role[] {
                new Role() {
                    Name = "Admin"
                }
            };
        }
        private Employee[] GenerateEmployees()
        {
            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < 200; i++)
            {
                Employee emp = new Employee()
                {
                    FirstName = MockData.Person.FirstName(),
                    LastName = MockData.Person.Surname(),
                    Email = MockData.Internet.Email(),
                    IsActive = i % 9 == 0 ? false : true,
                    Gender = i % 7 == 0 ? "M" : "F",
                    UniqueKey = Guid.NewGuid(),
                    CreatedDate = MockData.Utils.RandomDate(_dateFrom, _dateTo),
                    DepartmentID = MockData.RandomNumber.Next(1, 8)
                };
                employees.Add(emp);
            }

            return employees.ToArray();
        }
        private Stock[] GenerateStocks()
        {
            List<Stock> stocks = new List<Stock>();

            int devicesCount = MockDataInitializer.GenerateDevices().Count();

            for (int i = 1; i <= devicesCount; i++)
            {
                for (int j = 0; j < MockData.RandomNumber.Next(1, 10); j++)
                {
                    Stock stock = new Stock()
                    {
                        DeviceID = i,
                        UniqueKey = Guid.NewGuid(),
                        IsAvailable = true
                    };
                    stocks.Add(stock);
                }
            }

            return stocks.ToArray();
        }
        
    }
}
