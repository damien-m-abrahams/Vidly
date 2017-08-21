using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dto
{
	public class CustomerDto
	{
		public int Id { get; set; }

		[StringLength(255)] // Data annotations
		public string Name { get; set; }

		public DateTime? BirthDate { get; set; }

		public bool IsSubscribedToNewsletteer { get; set; }

		public byte MembershipTypeId { get; set; }
	}
}