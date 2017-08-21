using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
		private INavigationViewModel navigationViewModel;

		private ApplicationDbContext dbContext;

		public MoviesController(INavigationViewModel navigationViewModel, ApplicationDbContext dbContext)
		{
			if (navigationViewModel == null)
				throw new ArgumentNullException(nameof(navigationViewModel));
			if (dbContext == null)
				throw new ArgumentNullException(nameof(dbContext));

			this.navigationViewModel = navigationViewModel;
			this.dbContext = dbContext;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			dbContext.Dispose();
		}

		// GET: Movies
		public ActionResult Index()
		{
			var movieViewModels = new List<MovieViewModel>();
			var movies = dbContext.Movies.Include(m => m.GenreType);
			foreach (var movie in movies) {
				movieViewModels.Add(
					new MovieViewModel {
						Name = movie.Name,
						Genre = movie.GenreType.Name,
						DetailLink = new LinkViewModel {
							ActionName = "Edit",
							ActionProperties = new { id = movie.Id },
							ControllerName = "Movies"
						}
					});
			}

			var moviesViewModel = new MoviesViewModel
			{
				Navigation = this.navigationViewModel,
				NewMovie = new LinkViewModel {
					Display = "New Movie",
					ActionName = "New",
					ControllerName = "Movies",
				},
				Movies = movieViewModels
			};

			return View(moviesViewModel);
		}

		
		public ActionResult Detail(int id)
	    {
		    ViewResult result;

		    var movie = dbContext.Movies.Include(m => m.GenreType).SingleOrDefault(m => m.Id == id);
		    if (movie != null) {
			    var movieViewModel = new MovieViewModel {
				    Name = movie.Name,
				    Genre = movie.GenreType.Name,
					GeneralReleaseDate = movie.ReleaseDate,
					RentalReleaseDate = movie.DateAdded,
					Copies = movie.NumberInStock,
					// Details link not needed be view
				    DetailLink = new LinkViewModel {
					    ActionName = "Detail",
					    ActionProperties = new {id = movie.Id},
					    ControllerName = "Movies"
				    },
				    Navigation = navigationViewModel
			    };
			    result = View(movieViewModel);
			} else {
			    throw new InvalidOperationException("Detail index is out of bounds");
		    }

		    return result;
	    }

	    public ActionResult New()
	    {
			var genreTypes = dbContext.GenreTypes.ToArray();
			var movieFormViewModel = new MovieFormViewModel
			{
				Navigation = navigationViewModel,
				GenreTypes = genreTypes
			};
			return View("MovieForm", movieFormViewModel);
		}

		[HttpPost]
		public ActionResult Save(Movie movie)
		{
			ActionResult result;

			// All form fields are declared with Customer property names so we can bind to Customer instead of INewCustomerViewModel
			if (ModelState.IsValid) {
				if (movie.Id == 0) {
					dbContext.Movies.Add(movie);
				} else {
					var existingMovie = dbContext.Movies.Single(c => c.Id == movie.Id);

					existingMovie.Name = movie.Name;
					existingMovie.ReleaseDate = movie.ReleaseDate;
					existingMovie.GenreTypeId = movie.GenreTypeId;
					existingMovie.NumberInStock = movie.NumberInStock;
				}

				dbContext.SaveChanges();
				result = RedirectToAction("Index", "Movies");
			} else {
				var membershipTypes = dbContext.GenreTypes.ToArray();
				var movieFormViewModel = new MovieFormViewModel
				{
					Movie = movie,
					GenreTypes = membershipTypes,
					Navigation = navigationViewModel
				};

				result = View("MovieForm", movieFormViewModel);
			}

			return result;
		}

		public ActionResult Edit(int id)
		{
			ActionResult result;

			var membershipTypes = dbContext.GenreTypes.ToArray();
			var movie = dbContext.Movies.Include(m => m.GenreType).SingleOrDefault(c => c.Id == id);
			if (movie != null) {
				var movieFormViewModel = new MovieFormViewModel
				{
					Movie = movie,
					GenreTypes = membershipTypes,
					Navigation = navigationViewModel
				};
				result = View("MovieForm", movieFormViewModel);
			} else {
				result = HttpNotFound($"Could not find Movie {id}");
			}

			return result;
		}


		#region Introduction Methods
		// GET: Movies
		public ActionResult Random()
        {
	        var movie = new Movie {
				Name = "Shrek!"
	        };

			// When passing movie as a View argument internally this happens:
			// var viewResult = new ViewResult();
			// viewResult.ViewData.Model = movie;

			//return View(movie);
			//return Content("Yo MTYV Raps!");
			//return HttpNotFound("No movies available");
			//return new EmptyResult();
			//return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name"});

			//ViewBag.Movie = movie;
			//return View();

			var customers = new List<Customer> {
				new Customer {Name = "Customer 1"},
				new Customer {Name = "Customer 2"}
			};

			var viewModel = new RandomMovieViewModel
			{
				Movie = movie,
				Customers = customers
			};

			return View(viewModel);
		}

		/*
		public ActionResult Index(int? pageIndex, string sortBy)
		{
			if (!pageIndex.HasValue) {
				pageIndex = 1;
			}

			if (string.IsNullOrWhiteSpace(sortBy)) {
				sortBy = "Name";
			}

			return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
		}
		*/

		[Route("movies/releases/{year:regex(\\d{4}):range(1900, 2050)}/{month:regex(\\d{2}):range(1, 12)}")]
	    public ActionResult ByReleaseDate(int year, int month)
	    {
			// Constrain year and month values

		    return Content($"year={year}, month={month}");
	    }
		#endregion
	}
}