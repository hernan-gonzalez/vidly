using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using Vidly.Migrations;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        List<Movie> movies = new List<Movie>()
        {
            new Movie {Id=1,Name="Shrek" },
              new Movie {Id=2,Name="IronMan" }

        };
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose() ;
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!"};
     
            return View(movie);
            //return ViewResult(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genre = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Movie= new Movie(),
                Genre = genre,
                Header = "New Movie"
            };

            return View("MovieForm",viewModel);

        }

        [Authorize(Roles =RoleName.CanManageMovies)]
        public ActionResult Edit (int id)
        {
            // return Content("id=" + id);
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genre = _context.Genre.ToList(),
                Header = "Edit Movie"
            };

            return View("MovieForm", viewModel);

            
        }


        [HttpPost]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
               
                var viewModel = new MovieFormViewModel()
                {

                    Movie = movie,
                    Genre = _context.Genre.ToList(),
                    Header = "New Movie"
                };
                return View("MovieForm", viewModel);
            }


            if (movie.Id == 0)
            {
                movie.DateAdded=DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                //movieInDb.DateAdded=movie.date

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");

        }

  
        public ViewResult Index()
        {
            //  var movies = _context.Movies.Include(m => m.Genre).ToList();

            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            
            return View("ReadOnlyList");//View(movies);

        }

        [Route("Movies/Details/{id}")]
        public ActionResult Details(int id)
        {
            //Customer cust = customers.Find(s => s.Id == id);
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(s => s.Id == id);

            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        //public ActionResult Index()
        //{
        //    var viewModel = new MovieListViewModel()
        //    {
        //        Movies = movies
        //    };

        //    return View(viewModel);
        //}
    }
}