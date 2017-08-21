using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class MembershipType
	{
		public byte Id { get; set; }

		[Required]
		[StringLength(255)] // Data annotations
		public string Name { get; set; }

		public short SignUpFee { get; set; }

		public byte DurationInMonths { get; set; }

		public byte DiscountRate { get; set; }

		// Alternatively a enum can be used (but the enum value needs to be explicitly cast to byte)
		public static byte Unknown = 0;

		public static byte PayAsYouGo = 1;
	}
}