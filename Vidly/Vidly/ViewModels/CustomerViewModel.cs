namespace Vidly.ViewModels
{
	public class CustomerViewModel : ICustomerViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public string Name { get; set; }

		public string MembershipName { get; set; }

		public LinkViewModel DetailLink { get; set; }	
	}
}