using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class MemberAgeValidation : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			ValidationResult result;

			var customer = (Customer) validationContext.ObjectInstance;
			if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo) {
				result = ValidationResult.Success;
			} else {
				if (customer.BirthDate == null) {
					result = new ValidationResult("BirthDate is required");
				} else {
					var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
					result = (age >= 18)
						? ValidationResult.Success
						: new ValidationResult("Membership is only for customers who are at least 18 years old");
				}
			}

			return result;
		}
	}
}