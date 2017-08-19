using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public interface ICustomerFormViewModel : IViewModel
	{
		IEnumerable<MembershipType> MembershipTypes { get; set; }

		Customer Customer { get; set; }
	}
}