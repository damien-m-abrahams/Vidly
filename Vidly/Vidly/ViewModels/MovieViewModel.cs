namespace Vidly.ViewModels
{
	public class MovieViewModel : IMovieViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public string Name { get; set; }

		public string Genre { get; set; }

		public LinkViewModel DetailLink { get; set; }
	}
}