namespace DBRepositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        void Add(T obj);
        public void Remove(T entity);
        public T Update(T obj);
    }
}
