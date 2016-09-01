using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Models
{
    public class RentalViewModel
    {
        public RentalViewModel()
        {
            RentalHistories = new List<RentalHistoryViewModel>();
        }
        public IEnumerable<RentalHistoryViewModel> RentalHistories { get; set; }
        public IEnumerable<TotalRentalByDateViewModel> TotalRentalsByDate { get; set; }
    }
    public class RentalHistoryViewModel
    {
        public int ID { get; set; }
        public int StockID { get; set; }
        public DateTime RentalDate { get; set; }
        public Nullable<DateTime> ReturnedDate { get; set; }
        public string Status { get; set; }
        public string Employee { get; set; }
        public int EmployeeID { get; set; }
        public string Device { get; set; }
    }
    public class TotalRentalByDateViewModel
    {
        public DateTime Date { get; set; }
        public int TotalRentals { get; set; }
    }
}
