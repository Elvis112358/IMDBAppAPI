using EFModels;
using Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBRepositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MoviesDBContext context)
            : base(context)
        {

        }

        

        public PagedList<Movie> GetActiveItemsBySearch(SearchFeature searchFeature, PageParameters pageParameters)
        {
            var items = _dbSet.Where(m => m.Genre != "");

            //if (!string.IsNullOrEmpty(searchFeature.Name))
            // items = items.Where(i => i.MovieName.Contains(searchFeature.Name));
            // if (!string.IsNullOrEmpty(searchFeature.Genre))
            // items = items.Where(i => i.Genre == searchFeature.Genre);
            //if (!string.IsNullOrEmpty(searchFeature.Publisher))
            //    items = items.Where(i => i.Publisher.PublisherName == searchFeature.Publisher);

            // FOR STARS
            //if (!searchFeature.PriceFrom.Equals(null))
            //    items = items.Where(i => i.Price > searchFeature.PriceFrom);

            return PagedList<Movie>.ToPagedList(items, pageParameters.PageNumber, pageParameters.PageSize); ;
        }


        public override Movie GetById(object id)
        {
            return _dbSet.Include("ActorMovies.Actor").SingleOrDefault(i => i.MovieId == (int)id); 
        }
    }
}
