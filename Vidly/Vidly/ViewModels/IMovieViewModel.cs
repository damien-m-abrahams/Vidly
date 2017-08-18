using System;

namespace Vidly.ViewModels
{
	public interface IMovieViewModel : IViewModel
	{
		string Name { get; set; }

		string Genre { get; set; }

		DateTime GeneralReleaseDate { get; set; }

		DateTime RentalReleaseDate { get; set; }

		byte Copies { get; set; }

		LinkViewModel DetailLink { get; set; }
	}
}

