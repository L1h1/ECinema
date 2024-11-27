using ECinema.Data.MediatR.Cinema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ECinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CinemaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/cinemas
        [HttpGet]
        public async Task<IActionResult> GetAllCinemas()
        {
            var request = new SelectAllCinemasRequest();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // GET: api/cinemas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCinemaById(int id)
        {
            var request = new SelectCinemaByIdRequest { CinemaId = id };
            var result = await _mediator.Send(request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/cinemas
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCinema([FromBody] InsertCinemaRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        // PUT: api/cinemas/name
        [Authorize(Roles = "Admin")]
        [HttpPut("/name")]
        public async Task<IActionResult> UpdateCinemaName([FromBody] UpdateCinemaNameRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // PUT: api/cinemas/{id}/website
        [Authorize(Roles = "Admin")]
        [HttpPut("/website")]
        public async Task<IActionResult> UpdateCinemaWebsite(int id, [FromBody] UpdateCinemaWebsiteRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // DELETE: api/cinemas/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var request = new DeleteCinemaByIdRequest { CinemaId = id };
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
