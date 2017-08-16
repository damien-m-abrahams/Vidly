using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public class NavigationViewModel : INavigationViewModel
	{
		public string BrandName { get; set; }
		
		public string Version { get; set; }

		public LinkViewModel HomeLink { get; set; }

		public IList<LinkViewModel> ApplicationLinks { get; set; }

		public IList<LinkViewModel> AdministrationLinks { get; set; }
	}
}