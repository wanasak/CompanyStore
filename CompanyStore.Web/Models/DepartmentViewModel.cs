using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Models
{
    public class DepartmentViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfEmployee { get; set; }
    }
}
