using EFModels;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepositories
{
    public interface IMovieRepository
    {
        public PagedList<Movie> GetActiveItemsBySearch(SearchFeature searchFeature, PageParameters pageParameters);
        Movie GetById(object id);
        void Add(Movie obj);
        Movie Update(Movie obj);
        void Remove(Movie obj);
    }
}
