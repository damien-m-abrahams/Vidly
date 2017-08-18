using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class NewCustomerViewModel : INewCustomerViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public IEnumerable<MembershipType> MembershipTypes { get; set; }

		public Customer Customer { get; set; }
	}
}