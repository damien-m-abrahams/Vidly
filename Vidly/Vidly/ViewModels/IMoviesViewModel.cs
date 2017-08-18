using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public interface IMoviesViewModel : IViewModel
	{
		IEnumerable<IMovieViewModel> Movies { get; set; }
	}
}