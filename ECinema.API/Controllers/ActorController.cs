using ECinema.Data.MediatR.Actor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/actors
        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            var request = new SelectAllActorsRequest();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // GET: api/actors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActorById(int id)
        {
            var request = new SelectActorByIdRequest { ActorId = id };
            var result = await _mediator.Send(request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/actors
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateActor([FromBody] InsertActorRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetActorById), new { id = result }, result);
        }

        // PUT: api/actors
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateActor([FromBody] UpdateActorRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        // DELETE: api/actors/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var request = new DeleteActorRequest { ActorId = id };
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
