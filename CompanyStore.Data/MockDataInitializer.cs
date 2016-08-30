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
