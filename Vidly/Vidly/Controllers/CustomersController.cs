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
							ActionName = "Edit",
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
				return HttpNotFound($"Could not find Customer {id}");
			}

		    return result;
	    }

	    public ActionResult New()
	    {
		    var membershipTypes = dbContext.MembershipTypes.ToArray();
		    var customerFormFormViewModel = new CustomerFormFormViewModel {
			    Navigation = navigationViewModel,
				MembershipTypes = membershipTypes,
				Customer = new Customer()
		    };
		    return View("CustomerForm", customerFormFormViewModel);
	    }

		[HttpPost]
	    public ActionResult Save(Customer customer)
	    {
			// All form fields are declared with Customer property names so we can bind to Customer instead of INewCustomerViewModel
			if (ModelState.IsValid) {
				if (customer.Id == 0) {
					dbContext.Customers.Add(customer);
				} else {
					var existingCustomer = dbContext.Customers.Single(c => c.Id == customer.Id);

					// Use Automapper e.g. Mapper.Map(customer, existingCustomer)
					existingCustomer.Name = customer.Name;
					existingCustomer.BirthDate = customer.BirthDate;
					existingCustomer.MembershipTypeId = customer.MembershipTypeId;
					existingCustomer.IsSubscribedToNewsletteer = customer.IsSubscribedToNewsletteer;
				}

			    dbContext.SaveChanges();
			    return RedirectToAction("Index", "Customers");
		    } else {
			    throw new InvalidOperationException("Customer state is invalid");
		    }
	    }

		public ActionResult Edit(int id)
		{
			ViewResult result;

			var membershipTypes = dbContext.MembershipTypes.ToArray();
			var customer = dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
			if (customer != null) {
				var customerViewModel = new CustomerFormFormViewModel
				{
					Customer = customer,
					MembershipTypes = membershipTypes,
					Navigation = navigationViewModel
				};
				result = View("CustomerForm", customerViewModel);
			} else {
				return HttpNotFound($"Could not find Customer {id}");
			}

			return result;
		}
	}
}