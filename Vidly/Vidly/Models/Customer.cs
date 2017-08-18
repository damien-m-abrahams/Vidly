using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)] // Data annotations
		public string Name { get; set; }

		public DateTime? BirthDate { get; set; }

		public bool IsSubscribedToNewsletteer { get; set; }

		public MembershipType MembershipType { get; set; } // Navigation property

		[Display(Name = "Membership Type")]
		public byte MembershipTypeId { get; set; }
	}
}