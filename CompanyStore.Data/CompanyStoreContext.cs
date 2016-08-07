using CompanyStore.Data.Configuration;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data
{
    public class CompanyStoreContext : DbContext
    {
        public CompanyStoreContext()
            : base("CompanyStore")
        {
            Database.SetInitializer<CompanyStoreContext>(null);
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Device> Devices { get; set; }
        public IDbSet<Rental> Rentals { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Stock> Stocks { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<Employee> Employees { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new DeviceConfiguration());
            modelBuilder.Configurations.Add(new RentalConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new StockConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
        }
    }
}
