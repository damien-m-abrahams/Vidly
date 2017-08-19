using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public interface IMoviesViewModel : IViewModel
	{
		LinkViewModel NewMovie { get; set; }

		IEnumerable<IMovieViewModel> Movies { get; set; }
	}
}