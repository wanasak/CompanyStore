using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Models
{
    public class RentalHistoryViewModel
    {
        public int ID { get; set; }
        public int StockID { get; set; }
        public DateTime RentalDate { get; set; }
        public Nullable<DateTime> ReturnedDate { get; set; }
        public string Status { get; set; }
        public string Employee { get; set; }
        public string Device { get; set; }
    }
}
