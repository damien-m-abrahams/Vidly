using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
	public class CustomerViewModel : ICustomerViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public int Id { get; set; }

		public string Name { get; set; }

		[Display(Name = "Date of Birth")]
		public DateTime? BirthDate { get; set; }

		public string MembershipName { get; set; }

		public bool IsNewsLetterSubscriber { get; set; }

		public LinkViewModel DetailLink { get; set; }	
	}
}