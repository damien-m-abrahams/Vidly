namespace Vidly.ViewModels
{
	public interface ICustomerViewModel : IViewModel
	{
		string Name { get; set; }

		string MembershipName { get; set; }

		LinkViewModel DetailLink { get; set; }
	}
}