using System;

namespace Vidly.ViewModels
{
	public class MovieViewModel : IMovieViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public string Name { get; set; }

		public string Genre { get; set; }

		public DateTime GeneralReleaseDate { get; set; }

		public DateTime RentalReleaseDate { get; set; }

		public byte Copies { get; set; }

		public LinkViewModel DetailLink { get; set; }
	}
}