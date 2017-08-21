using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Column(TypeName = "datetime2")]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }

		[Column(TypeName = "datetime2")]
		[Display(Name = "Rental Release Data")]
		public DateTime DateAdded { get; set; }

		[Display(Name = "Number in Stock")]
		[Range(1, 20)]
		public byte NumberInStock { get; set; }

		public GenreType GenreType { get; set; } // Navigation property

		[Display(Name = "Genre")]
		[Required]
		public byte GenreTypeId { get; set; }
	}
}