using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DTO;
using EF = EFModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helpers;
using Newtonsoft.Json;

namespace IMDBApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly EF.MoviesDBContext _context;
        IMoviesDAL _movieDAL;

        public MoviesController(EF.MoviesDBContext context, IMoviesDAL movieDAL)
        {
            _context = context;
            _movieDAL = movieDAL;
        }


        // GET: api/Movies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        //{
        //    return await _context.Movies.ToListAsync();
        //}

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            var movie = _movieDAL.GetActiveItemsById(id);
            return Ok(movie);
        }

        // GET: api/Movies/search
        [HttpGet("search")]
        public  ActionResult<Movie[]> SearchMovie(int pageSize, int pageNumber, string name, string rate)
        {
            SearchFeature searchFeature = new SearchFeature()
            {
                Name = name,
                Genre = "horor",
                Publisher = "Warner Pictures"
            };

            PageParameters movieParameters = new PageParameters()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            

            PagedList<Movie> movies = _movieDAL.SearchProducts(searchFeature, movieParameters);
            var metada = new
            {
                movies.TotalCount,
                movies.PageSize,
                movies.CurrentPage,
                movies.HasNext,
                movies.HasPrevious,
                movies.TotalPages,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metada));

            // TODO
            // Add Rate attribute to order movies
            movies.OrderByDescending(x => x.MovieName);
            
            return Ok(movies);

        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Movie movie)
        {
            // apiValidationBll.ValidateAndUpdateNewProduct(product, true);
            var existingMovie = _movieDAL.GetActiveItemsById(id);
            if (existingMovie == null)
                return BadRequest( "There is no Movie added with requested Id ");
            var movieUpdated = _movieDAL.UpdateProduct(movie);
            return Ok(movieUpdated);
        }

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public  IActionResult PostMovie(Movie movie)
        {
            var addedMovie = _movieDAL.AddMovie(movie);
            return Ok(addedMovie);
        }

        //// DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public ActionResult<Movie> DeleteMovie(int id)
        {
           var existingMovie = _movieDAL.GetActiveItemsById(id);
            if (existingMovie == null)
                return BadRequest( "There is no Movie added with requested Id ");

            _movieDAL.Delete(existingMovie);

            return existingMovie;
        }

    }
}
