using ECinema.Data.MediatR.Movie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECinema.Data.MediatR.MovieActor;
using ECinema.Data.MediatR.MovieCinema;
using ECinema.Data.MediatR.MovieGenre;
using Microsoft.AspNetCore.Authorization;

namespace ECinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMovies(CancellationToken cancellationToken)
        {
            try
            {
                var movies = await _mediator.Send(new SelectAllMoviesRequest(), cancellationToken);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ошибка при получении фильмов: {ex.Message}");
            }
        }

        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var request = new SelectMovieByIdRequest { MovieId = id };
            var result = await _mediator.Send(request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/movies
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] InsertMovieRequest request)
        {
            var movieId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetMovieById), new { id = movieId }, null);
        }

        // PUT: api/movies
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateMovieDetails([FromBody] UpdateMovieDetailsRequest request)
        {
            var success = await _mediator.Send(request);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // PUT: api/movies/title
        [Authorize(Roles = "Admin")]
        [HttpPut("/title")]
        public async Task<IActionResult> UpdateMovieTitle([FromBody] UpdateMovieTitleRequest request)
        {
            var success = await _mediator.Send(request);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/movies/{movieId}/actor/{actorId}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{movieId}/actor/{actorId}")]
        public async Task<IActionResult> DeleteMovieActor(int movieId, int actorId)
        {
            var request = new DeleteMovieActorRequest { MovieId = movieId, ActorId = actorId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // DELETE: api/movies/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var request = new DeleteMovieByIdRequest { MovieId = id };
            var result = await _mediator.Send(request);
            if (!result)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/movies/{movieId}/cinema/{cinemaId}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{movieId}/cinema/{cinemaId}")]
        public async Task<IActionResult> DeleteMovieCinema(int movieId, int cinemaId)
        {
            var request = new DeleteMovieCinemaRequest { MovieId = movieId, CinemaId = cinemaId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // DELETE: api/movies/{movieId}/genre/{genreId}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{movieId}/genre/{genreId}")]
        public async Task<IActionResult> DeleteMovieGenre(int movieId, int genreId)
        {
            var request = new DeleteMovieGenreRequest { MovieId = movieId, GenreId = genreId };
            await _mediator.Send(request);
            return NoContent();
        }

        // POST: api/movies/actor
        [Authorize(Roles = "Admin")]
        [HttpPost("/actor")]
        public async Task<IActionResult> AddMovieActor([FromBody] InsertMovieActorRequest request)
        {
            var actorId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetMovieById), actorId);
        }

        // POST: api/movies/cinema
        [Authorize(Roles = "Admin")]
        [HttpPost("/cinema")]
        public async Task<IActionResult> AddMovieCinema([FromBody] InsertMovieCinemaRequest request)
        {
            var cinemaId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetMovieById), cinemaId);
        }

        // POST: api/movies/genre
        [Authorize(Roles = "Admin")]
        [HttpPost("/genre")]
        public async Task<IActionResult> AddMovieGenre([FromBody] InsertMovieGenreRequest request)
        {
            var genreId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetMovieById), genreId);
        }

        // GET: api/movies/actor/{actorId}
        [HttpGet("actor/{actorId}")]
        public async Task<IActionResult> GetMoviesByActor(int actorId)
        {
            var request = new SelectMoviesByActorRequest { ActorId = actorId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // GET: api/movies/cinema/{cinemaId}
        [HttpGet("cinema/{cinemaId}")]
        public async Task<IActionResult> GetMoviesByCinema(int cinemaId)
        {
            var request = new SelectMoviesByCinemaRequest { CinemaId = cinemaId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // GET: api/movies/genre/{genreId}
        [HttpGet("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var request = new SelectMoviesByGenreRequest { GenreId = genreId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
