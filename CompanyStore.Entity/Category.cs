using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Entity
{
    public class Category : IEntityBase
    {
        public Category()
        {
            Devices = new List<Device>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Device> Devices { get; set; }    
    }
}
