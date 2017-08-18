using System;

namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime ReleaseDate { get; set; }

		public DateTime DateAdded { get; set; }

		public byte NumberInStock { get; set; }

		public GenreType GenreType { get; set; } // Navigation property

		public byte GenreTypeId { get; set; }
	}
}