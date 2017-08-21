using System;
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

	    // GET /api/customers
	    [HttpGet]
	    public async Task<IHttpActionResult> Customers()
	    {
		    var customerDtos = await dbContext.Customers.ProjectTo<CustomerDto>().ToListAsync();
		    return Ok(customerDtos);
	    }

	    // GET /api/customers/{id}
		[HttpGet]
		public async Task<IHttpActionResult> Customer(int id)
		{
			IHttpActionResult result;

			var customer = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);

			if (customer != null) {
				var customerDto = Mapper.Map<Customer, CustomerDto>(customer);
				result = Ok(customerDto);
			} else {
				result = NotFound();
			}

			return result;
		}

		// POST /api/customers
		[HttpPost]
	    public async Task<IHttpActionResult> CreateCustomer(CustomerDto customerDto)
		{
			IHttpActionResult result;

		    if (ModelState.IsValid) {
			    var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
			    dbContext.Customers.Add(customer);
			    await dbContext.SaveChangesAsync();
				customerDto.Id = customer.Id;
			    result = Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
		    } else {
			    result = BadRequest("Customer DTO is invalid");
		    }

			return result;
	    }

		// PUT /api/customers/{id}
		[HttpPut]
		public async Task UpdateCustomer(int id, CustomerDto customerDto)
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

		// DELETE /api/customers/{id}
		[HttpDelete]
	    public async Task RemoveCustomer(int id)
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
