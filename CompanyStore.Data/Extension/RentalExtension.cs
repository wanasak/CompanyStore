using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Extension
{
    public static class RentalExtension
    {
        public static IEnumerable<Rental> GetRentalByEmployeeID(this IEntityBaseRepository<Rental> rentalRepository, int employeeID)
        {
            return rentalRepository.GetAll().Where(r => r.EmployeeID == employeeID);
        }
    }
}
