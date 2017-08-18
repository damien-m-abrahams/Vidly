using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class GenreType
	{
		public byte Id { get; set; }

		[Required]
		[StringLength(255)] // Data annotations
		public string Name { get; set; }
	}
}