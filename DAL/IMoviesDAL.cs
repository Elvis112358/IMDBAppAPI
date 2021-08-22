using DTO;
using Helpers;
using System.Collections.Generic;

namespace DAL
{
    public interface IMoviesDAL
    {
        public PagedList<Movie> SearchProducts(SearchFeature searchQuery, PageParameters movieParameters);
        public Movie GetActiveItemsById(int id);
        public Movie AddMovie(Movie movie);
        public Movie UpdateProduct(Movie movie);
        public void Delete(Movie movie);

    }
}
