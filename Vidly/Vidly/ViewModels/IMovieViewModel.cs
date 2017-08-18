namespace Vidly.ViewModels
{
	public interface IMovieViewModel : IViewModel
	{
		string Name { get; set; }

		string Genre { get; set; }

		LinkViewModel DetailLink { get; set; }
	}
}

