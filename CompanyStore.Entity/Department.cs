using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Entity
{
    public class Department : IEntityBase
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
