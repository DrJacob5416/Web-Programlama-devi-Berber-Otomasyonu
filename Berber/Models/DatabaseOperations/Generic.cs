using Berber.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Berber.Models.DatabaseOperations
{
    public class Generic<T> : IGeneric<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        internal DbSet<T> dbSet;

        public Generic(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this.dbSet = _applicationDbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> request = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    request = request.Include(prop);
                }
            }
            return request.ToList();
        }

        public IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> request = dbSet;
            request = request.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    request = request.Include(prop);
                }
            }
            return request.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetByIdWithProps(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> request = dbSet;
            request = request.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    request = request.Include(prop);
                }
            }
            return request.SingleOrDefault();
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
