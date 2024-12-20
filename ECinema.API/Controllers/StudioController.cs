using ECinema.Data.Entities;
using ECinema.Data.MediatR.Genre;
using ECinema.Data.MediatR.Studio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : Controller
    {

        private readonly IMediator _mediator;

        public StudioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Studio>>> GetAllStudios()
        {
            var studios = await _mediator.Send(new SelectAllStudiosRequest());
            if (studios == null || studios.Count == 0)
            {
                return NotFound("No studios found.");
            }
            return Ok(studios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> GetStudioById(int id)
        {
            var studio = await _mediator.Send(new SelectStudioByIdRequest() { StudioId = id });
            if (studio == null)
            {
                return NotFound("No studios found.");
            }
            return Ok(studio);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> InsertStudio([FromBody] Studio studio)
        {
            var result = await _mediator.Send(new AddStudioRequest
            {
                StudioName = studio.StudioName
            });

            return CreatedAtAction(nameof(GetAllStudios), new { id = result }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudioById([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteStudioByIdRequest { StudioId = id });

            if (!result)
            {
                return NotFound("Studio not found.");
            }

            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateStudio([FromBody] Studio studio)
        {
            var result = await _mediator.Send(new UpdateStudioNameRequest
            {
                StudioId = studio.StudioId,
                StudioName = studio.StudioName
            });

            if (!result)
            {
                return NotFound("Studio not found.");
            }

            return NoContent();
        }

















    }
}
