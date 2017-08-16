using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public class CustomersViewModel : ICustomersViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public IEnumerable<ICustomerViewModel> Customers { get; set; }
	}
}