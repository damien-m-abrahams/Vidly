using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Vidly
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			// Install AutoMapper package
			//
			// Create profile:
			//
			//public class AutoMapperProfile : Profile
			//{
			//	public AutoMapperProfile() {
			//		CreateMap<ModelType, DtoType>();
			//	}
			//}
			//
			// In Controller method:
			//
			// var modelDto = Mapper.Map<ModelDto>(ModelInstance);
			// or
			// var modelDtoCollection = Mapper.Map<ModelDto>([] { ModelIntance1, ModelInstance2 });
			//
			//AutoMapper.Mapper.Initialize(config => config.AddProfile<AutomapperProfile>());
			

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			UnityConfig.RegisterComponents();
		}
	}
}
