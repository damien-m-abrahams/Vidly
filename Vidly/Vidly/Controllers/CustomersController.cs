using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
		private INavigationViewModel navigationViewModel;

		private readonly ICustomersViewModel customersViewModel;

	    private ApplicationDbContext dbContext;

		public CustomersController(INavigationViewModel navigationViewModel, ApplicationDbContext dbContext)
		{
			if (navigationViewModel == null)
				throw new ArgumentNullException(nameof(navigationViewModel));
			if (dbContext == null)
				throw new ArgumentNullException(nameof(dbContext));

			this.navigationViewModel = navigationViewModel;
			this.dbContext = dbContext;

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

	    protected override void Dispose(bool disposing)
	    {
		    base.Dispose(disposing);
			dbContext.Dispose();
	    }

	    // GET: Customers
		public ActionResult Index()
		{
			var customerViewModels = new List<CustomerViewModel>();
			var customers = dbContext.Customers;
			foreach (var customer in customers) {
				customerViewModels.Add(
					new CustomerViewModel {
						Name = customer.Name,
						DetailLink = new LinkViewModel {
							ActionName = "Detail",
							ActionProperties = new {id = customer.Id},
							ControllerName = "Customers"
						}
					});
			}
			customersViewModel.Customers = customerViewModels;

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