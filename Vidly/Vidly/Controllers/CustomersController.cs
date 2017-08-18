using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
		private INavigationViewModel navigationViewModel;

	    private ApplicationDbContext dbContext;

		public CustomersController(INavigationViewModel navigationViewModel, ApplicationDbContext dbContext)
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

	    // GET: Customers
		public ActionResult Index()
		{
			var customerViewModels = new List<CustomerViewModel>();
			var customers = dbContext.Customers.Include(c => c.MembershipType); // Include eager loads associated membership types
			foreach (var customer in customers) {
				customerViewModels.Add(
					new CustomerViewModel {
						Name = customer.Name,
						BirthDate = customer.BirthDate,
						MembershipName = customer.MembershipType.Name,
						DetailLink = new LinkViewModel {
							ActionName = "Detail",
							ActionProperties = new {id = customer.Id},
							ControllerName = "Customers"
						}
					});
			}
			var customersViewModel = new CustomersViewModel
			{
				Navigation = navigationViewModel,
				Customers = customerViewModels
			};

            return View(customersViewModel);
        }

	    public ActionResult Detail(int id)
	    {
		    ViewResult result;

		    var customer = dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
		    if (customer != null) {
			    var customerViewModel = new CustomerViewModel {
				    Name = customer.Name,
				    BirthDate = customer.BirthDate,
				    MembershipName = customer.MembershipType.Name,
				    DetailLink = new LinkViewModel {
					    ActionName = "Detail",
					    ActionProperties = new {id = customer.Id},
					    ControllerName = "Customers"
				    },
				    Navigation = navigationViewModel
			    };
			    result = View(customerViewModel);
			} else {
			    throw new InvalidOperationException("Detail index is out of bounds");
		    }

		    return result;
	    }

	    public ActionResult New()
	    {
		    var customerViewModel = new CustomerViewModel {
			    Navigation = navigationViewModel
		    };
		    return View(customerViewModel);
	    }

	    public ActionResult Create()
	    {
		    throw new NotImplementedException();
	    }
    }
}