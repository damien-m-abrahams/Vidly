namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsSubscribedToNewsletteer { get; set; }

		public MembershipType MembershipType { get; set; } // Navigation property

		public byte MembershipTypeId { get; set; }
	}
}