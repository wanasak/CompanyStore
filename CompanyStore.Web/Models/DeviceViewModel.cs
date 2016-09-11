using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Models
{
    public class DeviceViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required Name."), MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Required Created Date.")]
        public DateTime CreatedDate { get; set; }
        public bool IsAvailable { get; set; }
        public int NumberOfStocksAvaiable { get; set; }
        public int NumberOfStocks { get; set; }
        public decimal Price { get; set; }
    }
}
