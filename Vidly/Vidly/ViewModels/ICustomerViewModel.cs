using System;

namespace Vidly.ViewModels
{
	public interface ICustomerViewModel : IViewModel
	{
		int Id { get; set; }

		string Name { get; set; }

		DateTime? BirthDate { get; set; }

		string MembershipName { get; set; }

		bool IsNewsLetterSubscriber { get; set; }

		LinkViewModel DetailLink { get; set; }
	}
}