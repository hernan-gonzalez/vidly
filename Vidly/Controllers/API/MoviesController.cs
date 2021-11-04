using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;


namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;


        public MoviesController()
        {
            _context = new ApplicationDbContext();

        }


        //GET /api/movies
        public IHttpActionResult GetMovies(string query=null)
        {

            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m=> m.NumberAvailable>0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));


            var moviesDtos = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDtos) ;

        }

        //GET /api/movies/1

        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));

        }


        //GET /api/movies/1
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        //PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            return Ok(_context.SaveChanges());

            
        }

        //Delete /api/customers/1
        public IHttpActionResult DeleteCustomer(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();
            _context.Movies.Remove(movieInDb);
            return Ok(_context.SaveChanges());

       

        }

    }
}
