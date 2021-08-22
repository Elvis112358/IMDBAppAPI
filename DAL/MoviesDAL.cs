using AutoMapper;
using DBRepositories;
using DTO;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UnitOfWork;
using EF = EFModels;

namespace DAL
{
    public class MoviesDAL :IMoviesDAL
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _uow;

        public MoviesDAL(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public Movie UpdateProduct(Movie movie)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.Movie efMovie = _mapper.Map<EF.Movie>(movie);

                //foreach (var actor in efMovie.ActorMovies)
                //{
                //    if (_uow.Actors.GetById(actor.ActorId) != null)
                //    {
                //        _uow.Actors.Update(actor.Actor);
                //    }
                //    else;
                //    _uow.Actors.Add(actor.Actor);
                //}

                efMovie.ActorMovies.Clear();
                var updateMovie = _uow.Movies.Update(efMovie);

                _uow.Commit();
                return _mapper.Map<Movie>(updateMovie);
            }
        }

        public Movie GetActiveItemsById(int id)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                var efMovie = _uow.Movies.GetById(id);
                Movie movie = _mapper.Map<Movie>(efMovie);
               
                return movie;
            }
        }

        public PagedList<Movie> SearchProducts(SearchFeature searchFeature, PageParameters movieParameters)
        {
            // using (_iow.Context = new EF.PersonalProjectAPContext())
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.SearchFeature efSearchFeature = _mapper.Map<EF.SearchFeature>(searchFeature);
                var efMovie = _uow.Movies.GetActiveItemsBySearch(efSearchFeature, movieParameters);
                Movie[] itemVersion = _mapper.Map<Movie[]>(efMovie);
                return _mapper.Map<PagedList<Movie>>(itemVersion);
            }
        }

        public void Delete(Movie movie)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.Movie efMovie = _mapper.Map<EF.Movie>(movie);
                _uow.Movies.Remove(efMovie);
            }
        }
        public Movie AddMovie(Movie movie)
        {
            using (_uow.Context = new EF.MoviesDBContext())
            {
                EF.Movie efMovie = new EF.Movie()
                {
                    MovieName = movie.MovieName,
                    Genre = movie.Genre,
                    Duration = movie.Duration,
                    Image = movie.Image
                };

                foreach (var actor in movie.Actors)
                {
                    efMovie.ActorMovies.Add(new EF.ActorMovie()
                    {
                        ActorId = actor.ActorId
                    });
                }
                // efMovie.Publisher = _uow.
                _uow.Movies.Add(efMovie);

                _uow.Commit();
                return movie;
            }
        }
    }
}
