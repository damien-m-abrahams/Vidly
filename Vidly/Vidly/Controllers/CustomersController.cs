using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
		private INavigationViewModel navigationViewModel;

		private readonly ICustomersViewModel customersViewModel;

		public CustomersController(INavigationViewModel navigationViewModel)
		{
			if (navigationViewModel == null)
				throw new ArgumentNullException(nameof(navigationViewModel));

			this.navigationViewModel = navigationViewModel;

			// TODO Build view model from Customer objects
			customersViewModel = new CustomersViewModel {
				Navigation = this.navigationViewModel,
				Customers = new[] {
					new CustomerViewModel {
						Name = "John Smith",
						DetailLink = new LinkViewModel {
							ActionName = "Detail",
							ActionProperties = new {id = 1},
							ControllerName = "Customers"
						}
					},
					new CustomerViewModel {
						Name = "Mary Williams",
						DetailLink = new LinkViewModel {
							ActionName = "Detail",
							ActionProperties = new {id = 2},
							ControllerName = "Customers"
						}
					},
				}
			};
		}

		// GET: Customers
		public ActionResult Index()
		{
            return View(customersViewModel);
        }

	    public ActionResult Detail(int id)
	    {
		    ViewResult result;

		    var index = id - 1;
		    if (index >= 0 && index < customersViewModel.Customers.Count()) {
			    var customerViewModel = customersViewModel.Customers.ElementAt(id - 1);
			    customerViewModel.Navigation = navigationViewModel;
			    result = View(customerViewModel);
		    } else {
			    throw new InvalidOperationException("Detail index is out of bounds");
		    }

		    return result;
	    }
    }
}