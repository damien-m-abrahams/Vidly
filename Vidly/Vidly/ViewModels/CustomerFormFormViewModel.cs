﻿using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class CustomerFormFormViewModel : ICustomerFormViewModel
	{
		public INavigationViewModel Navigation { get; set; }

		public IEnumerable<MembershipType> MembershipTypes { get; set; }

		public Customer Customer { get; set; }
	}
}