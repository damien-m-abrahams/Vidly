using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
		private INavigationViewModel navigationViewModel;

		private readonly IMoviesViewModel moviesViewModel;

		public MoviesController(INavigationViewModel navigationViewModel)
		{
			if (navigationViewModel == null)
				throw new ArgumentNullException(nameof(navigationViewModel));

			this.navigationViewModel = navigationViewModel;

			moviesViewModel = new MoviesViewModel
			{
				Navigation = this.navigationViewModel
			};
		}

		// GET: Movies
		public ActionResult Index()
	    {
			return View(moviesViewModel);
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

	    public ActionResult Edit(int id)
	    {
		    return Content("id=" + id);
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