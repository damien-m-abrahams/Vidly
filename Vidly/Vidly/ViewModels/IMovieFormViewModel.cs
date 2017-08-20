using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public interface IMovieFormViewModel : IViewModel
	{
		IEnumerable<GenreType> GenreTypes { get; set; }

		Movie Movie { get; set; }
	}
}
