using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
namespace Vidly.Controllers.API
{
    public class RentMoviesController : ApiController
    {


        private ApplicationDbContext _context;


        public RentMoviesController()
        {
            _context = new ApplicationDbContext();
        }


       
        [HttpPost]
        public IHttpActionResult CreateRentMovies(RentMoviesDto rentMovies)
        {
           // throw new NotImplementedException();

            //if (!ModelState.IsValid)
            //    return BadRequest();

            var customer = _context.Customers.Single(c => c.Id == rentMovies.CustomerId);

         
            var movies = _context.Movies.Where(m => rentMovies.MovieIds.Contains(m.Id)).ToList();
            

            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");


                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rental.Add(rental);
                movie.NumberAvailable--;
                
            }
            _context.SaveChanges();

            return Ok();

        }





        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}