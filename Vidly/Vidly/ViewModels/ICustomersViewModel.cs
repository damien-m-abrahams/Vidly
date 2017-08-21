using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public interface ICustomersViewModel : IViewModel
	{
		LinkViewModel NewCustomer { get; set; }

		IEnumerable<ICustomerViewModel> Customers { get; set; }
	}
}