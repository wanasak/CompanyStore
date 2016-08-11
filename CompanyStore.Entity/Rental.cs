using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Entity
{
    public class Rental : IEntityBase
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public int StockID { get; set; }
        public virtual Stock Stock { get; set; }
        public DateTime RentalDate { get; set; }
        public Nullable<DateTime> ReturnedDate { get; set; }
        public string Status { get; set; }
    }
}
