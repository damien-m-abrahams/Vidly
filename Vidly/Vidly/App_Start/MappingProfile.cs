using AutoMapper;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, CustomerDto>();
			CreateMap<CustomerDto, Customer>();
		}
	}
}