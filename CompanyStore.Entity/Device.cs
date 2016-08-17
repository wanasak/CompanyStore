using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Entity
{
    public class Device : IEntityBase
    {
        public Device()
        {
            Stocks = new List<Stock>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public decimal Price { get; set; }
    }
}
