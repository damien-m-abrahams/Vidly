using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
	    private ApplicationDbContext dbContext;

	    public CustomersController()
	    {
			// TODO Inject ApplicationDbContext via Unity - Unity.WebApi (WebApiConfig) is different to Unity.Mvc (UnityConfig)
		    dbContext = new ApplicationDbContext();
	    }

	    // GET /api/customers/all
	    [HttpGet]
	    public async Task<IEnumerable<CustomerDto>> All()
	    {
		    var customerDtos = await dbContext.Customers.ProjectTo<CustomerDto>().ToListAsync();
		    return customerDtos;
	    }

	    // GET /api/customeer/single/{id}
		[HttpGet]
		public async Task<CustomerDto> Single(int id)
		{
			var customer = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);

			if (customer == null) {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			return Mapper.Map<Customer, CustomerDto>(customer);
		}

		// POST /api/customers/create
		[HttpPost]
	    public async Task<CustomerDto> Create(CustomerDto customerDto)
	    {
		    if (ModelState.IsValid) {
			    var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
			    dbContext.Customers.Add(customer);
			    await dbContext.SaveChangesAsync();
				customerDto.Id = customer.Id;
		    } else {
			    throw new HttpResponseException(HttpStatusCode.BadRequest);
		    }

			return customerDto;
	    }

		// POST /api/customers/update/{id}
		[HttpPut]
		public async Task Update(int id, CustomerDto customerDto)
		{
			if (ModelState.IsValid) {
				var customerInDb = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
				if (customerInDb != null) {
					Mapper.Map(customerDto, customerInDb);
				} else {
					throw new HttpResponseException(HttpStatusCode.NotFound);
				}
				await dbContext.SaveChangesAsync();
			} else {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
		}

		// DELETE /api/customers/remove/{id}
		[HttpDelete]
	    public async Task Remove(int id)
	    {
			var customerInDb = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
			if (customerInDb != null) {
				dbContext.Customers.Remove(customerInDb);
				await dbContext.SaveChangesAsync();
			} else {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
		}
	}
}
