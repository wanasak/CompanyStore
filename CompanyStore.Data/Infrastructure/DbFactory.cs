using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CompanyStoreContext dbContext;

        public CompanyStoreContext Init()
        {
            return dbContext ?? (dbContext = new CompanyStoreContext());
        }

        protected override void DisposeCore() 
        { 
            if (dbContext != null) 
                dbContext.Dispose(); 
        }
    }
}
