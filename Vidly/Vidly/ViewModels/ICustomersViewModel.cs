using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public interface ICustomersViewModel : IViewModel
	{
		IEnumerable<ICustomerViewModel> Customers { get; set; }
	}
}