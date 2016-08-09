using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Infrastructure.Core
{
    public class Pagination<T>
    {
        public int draw { get; set; }
        public int recordFiltered { get; set; }
        public int recordTotal { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}
