using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public interface INewCustomerViewModel : IViewModel
	{
		IEnumerable<MembershipType> MembershipTypes { get; set; }

		Customer Customer { get; set; }
	}
}