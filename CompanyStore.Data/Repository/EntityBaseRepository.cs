using CompanyStore.Data.Infrastructure;
using CompanyStore.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Data.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private CompanyStoreContext dbContext;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected CompanyStoreContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>(); 
            foreach (var includeProperty in includeProperties) 
            { 
                query = query.Include(includeProperty); 
            } 
            return query;
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public virtual IQueryable<T> All
        {
            get { return GetAll(); }
        }

        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry entry = DbContext.Entry<T>(entity);
            DbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entry = DbContext.Entry<T>(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void Edit(T entity)
        {
            DbEntityEntry entry = DbContext.Entry<T>(entity);
            entry.State = EntityState.Modified;
        }
    }
}
