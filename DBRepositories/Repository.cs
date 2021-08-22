using EFModels;
using Microsoft.EntityFrameworkCore;


namespace DBRepositories
{
    public abstract class Repository<T> : IRepository<T>
         where T : class
    {
        protected DbSet<T> _dbSet;

        public Repository(MoviesDBContext context)
        {
            _dbSet = context.Set<T>();
        }

        public abstract T GetById(object id);

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }


    }
}
