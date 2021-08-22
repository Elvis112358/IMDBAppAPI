using EFModels;
using DBRepositories;


namespace UnitOfWork
{
    public interface IUnitOfWork
    {
        public MoviesDBContext Context { get; set; }
        IMovieRepository Movies { get; }
        IRepository<Actor> Actors { get; }
        IUserRepository Users { get; }
        void Commit();
    }
}

