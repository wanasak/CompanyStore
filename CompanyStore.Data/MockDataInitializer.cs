using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data
{
    public static class MockDataInitializer
    {
        public static Category[] GenerateCategories()
        {
            Category[] categories = new Category[] {
                new Category() { Name = "Tablet" },
                new Category() { Name = "Laptop" },
                new Category() { Name = "Desktop" },
                new Category() { Name = "Mobile" },
                new Category() { Name = "Monitor" },
                new Category() { Name = "UPS" },
                new Category() { Name = "Credit Card Devices" },
                new Category() { Name = "Other" },
                new Category() { Name = "Network" },
                new Category() { Name = "Printer" },
            };

            return categories;
        }
        public static Department[] GenerateDepartments()
        {
            DateTime fromDate = DateTime.Now.AddYears(-15);
            DateTime toDate = DateTime.Now;

            List<Department> departments = new List<Department>()
            {
                new Department()
                {
                    Name = "Engineering",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Economics",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Mathematics",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "English",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Nurse",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Medicine",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                },
                new Department()
                {
                    Name = "Human",
                    StartDate = MockData.Utils.RandomDate(fromDate, toDate)
                }
            };

            return departments.ToArray();
        }
    }
}
