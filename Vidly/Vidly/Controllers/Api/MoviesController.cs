using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
		private ApplicationDbContext dbContext;

		public MoviesController()
		{
			// TODO Inject ApplicationDbContext via Unity - Unity.WebApi (WebApiConfig) is different to Unity.Mvc (UnityConfig)
			dbContext = new ApplicationDbContext();
		}

		// GET /api/movies
		[HttpGet]
		public async Task<IHttpActionResult> Movies()
		{
			var movieDtos = await dbContext.Movies.ProjectTo<MovieDto>().ToListAsync();
			return Ok(movieDtos);
		}

		// GET /api/movies/{id}
		[HttpGet]
		public async Task<IHttpActionResult> Movie(int id)
		{
			IHttpActionResult result;

			var movie = await dbContext.Movies.SingleOrDefaultAsync(c => c.Id == id);

			if (movie != null) {
				var movieDto = Mapper.Map<Movie, MovieDto>(movie);
				result = Ok(movieDto);
			} else {
				result = NotFound();
			}

			return result;
		}

		// POST /api/movies
		[HttpPost]
		public async Task<IHttpActionResult> CreateMovie(MovieDto movieDto)
		{
			IHttpActionResult result;

			if (ModelState.IsValid) {
				var movie = Mapper.Map<MovieDto, Movie>(movieDto);
				dbContext.Movies.Add(movie);
				await dbContext.SaveChangesAsync();
				movieDto.Id = movie.Id;
				result = Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
			} else {
				result = BadRequest("Movie DTO is invalid");
			}

			return result;
		}

		// PUT /api/movies/{id}
		[HttpPut]
		public async Task UpdateMovie(int id, MovieDto movieDto)
		{
			if (ModelState.IsValid) {
				var movieInDb = await dbContext.Movies.SingleOrDefaultAsync(c => c.Id == id);
				if (movieInDb != null) {
					Mapper.Map(movieDto, movieInDb);
				} else {
					throw new HttpResponseException(HttpStatusCode.NotFound);
				}
				await dbContext.SaveChangesAsync();
			} else {
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
		}

		// DELETE /api/movies/{id}
		[HttpDelete]
		public async Task RemoveMovie(int id)
		{
			var movieInDb = await dbContext.Movies.SingleOrDefaultAsync(c => c.Id == id);
			if (movieInDb != null) {
				dbContext.Movies.Remove(movieInDb);
				await dbContext.SaveChangesAsync();
			} else {
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
		}
	}
}
