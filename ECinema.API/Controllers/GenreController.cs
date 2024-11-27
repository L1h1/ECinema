using ECinema.Data.Entities;
using ECinema.Data.MediatR.Genre;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Эндпоинт для получения всех жанров
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> GetAllGenres()
        {
            var genres = await _mediator.Send(new SelectAllGenresRequest());
            if (genres == null || genres.Count == 0)
            {
                return NotFound("No genres found.");
            }
            return Ok(genres);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> InsertGenre([FromBody] Genre genre)
        {
            var result = await _mediator.Send(new AddGenreRequest
            {
                GenreName = genre.GenreName
            });

            return CreatedAtAction(nameof(GetAllGenres), new { id = result }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenreById([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteGenreByIdRequest { GenreId = id });

            if (!result)
            {
                return NotFound("Genre not found.");
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("name/{name}")]
        public async Task<ActionResult> DeleteGenreByName([FromRoute] string name)
        {
            var result = await _mediator.Send(new DeleteGenreByNameRequest { GenreName = name });

            if (!result)
            {
                return NotFound("Genre not found.");
            }

            return NoContent();
        }
    }
}
