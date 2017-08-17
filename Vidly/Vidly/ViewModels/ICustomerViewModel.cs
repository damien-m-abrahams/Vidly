namespace Vidly.ViewModels
{
	public interface ICustomerViewModel : IViewModel
	{
		string Name { get; set; }

		byte Discount { get; set; }

		LinkViewModel DetailLink { get; set; }
	}
}