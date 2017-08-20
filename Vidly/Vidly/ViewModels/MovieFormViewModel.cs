using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class MovieFormViewModel : IMovieFormViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public IEnumerable<GenreType> GenreTypes { get; set; }

		public Movie Movie { get; set; }
	}
}