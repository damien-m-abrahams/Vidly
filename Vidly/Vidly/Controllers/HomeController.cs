using System;
using System.Web.Mvc;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class HomeController : Controller
	{
		private INavigationViewModel navigationViewModel;

		private readonly IHomeViewModel homeViewModel;

		public HomeController(INavigationViewModel navigationViewModel)
		{
			if (navigationViewModel == null) throw new ArgumentNullException(nameof(navigationViewModel));

			this.navigationViewModel = navigationViewModel;

			homeViewModel = new HomeViewModel {
				Navigation = this.navigationViewModel
			};
		}

		public ActionResult Index()
		{
			return View(homeViewModel);
		}
	}
}