using DBRepositories;
using EFModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MoviesDBContext context;
        private IMovieRepository movies;
        private IRepository<Actor> actors;
        private IUserRepository users;
        public MoviesDBContext Context
        {
            get
            {
                return context ?? throw new InvalidOperationException("Initialize Context property before using UOW.");
            }
            set
            {
                context = value;

                movies = null;
                actors = null;
                users = null;
            }
        }
       
        public IMovieRepository Movies
        {
            get
            {
                if (movies == null)
                {
                    movies = new MovieRepository(Context);
                }
                return movies;
            }
        }
    
        public IRepository<Actor> Actors
        {
            get
            {
                if (actors == null)
                {
                    actors = new ActorRepository(Context);
                }
                return actors;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (users == null)
                {
                    users = new UserRepository(Context);
                }
                return users;
            }
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}
