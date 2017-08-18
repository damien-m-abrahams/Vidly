using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public class MoviesViewModel : IMoviesViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public IEnumerable<IMovieViewModel> Movies { get; set; }
	}
}