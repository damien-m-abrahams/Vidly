using System.Collections.Generic;

namespace Vidly.ViewModels
{
	public interface INavigationViewModel
	{
		string BrandName { get; set; }

		string Version { get; set; }

		LinkViewModel HomeLink { get; set; }

		IList<LinkViewModel> ApplicationLinks { get; set; }

		IList<LinkViewModel> AdministrationLinks { get; set; }
	}
}