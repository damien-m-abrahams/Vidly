namespace Vidly.ViewModels
{
	public interface ICustomerViewModel : IViewModel
	{
		string Name { get; set; }

		LinkViewModel DetailLink { get; set; }
	}
}