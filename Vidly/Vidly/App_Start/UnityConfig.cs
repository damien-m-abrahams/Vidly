using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			#region Navigation View Model Factory

	        const string brandName = "Vidly";

	        var homeLink = new LinkViewModel {
		        ActionName = "Index",
		        ControllerName = "Home",
		        Display = brandName
	        };

			var applicationLinks = new List<LinkViewModel> {
				new LinkViewModel {
					ActionName = "Index",
					ControllerName = "Customers",
					Display = "Customers"
				},
				new LinkViewModel {
					ActionName = "Index",
					ControllerName = "Movies",
					Display = "Movies"
				}
			};

			var administrationLinks = new List<LinkViewModel>();

			var navigationViewModel = new NavigationViewModel
			{
				BrandName = brandName,
				Version = "0.1.0",
				HomeLink = homeLink,
				ApplicationLinks = applicationLinks,
				AdministrationLinks = administrationLinks
			};

			#endregion

			container.RegisterInstance(typeof(INavigationViewModel), navigationViewModel, new ContainerControlledLifetimeManager());
	        
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}