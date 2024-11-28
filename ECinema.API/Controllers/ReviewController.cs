using ECinema.Data.MediatR.Review;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ECinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/reviews/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var request = new SelectReviewByIdRequest { ReviewId = id };
            var result = await _mediator.Send(request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // GET: api/reviews/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetReviewsByMovie(int movieId)
        {
            var request = new SelectReviewsByMovieRequest { MovieId = movieId };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // POST: api/reviews
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] InsertReviewRequest request)
        {

            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && userIdFromToken != request.UserId.ToString())
            {
                return Forbid();
            }

            var reviewId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetReviewById), new { id = reviewId }, null);
        }

        // PUT: api/reviews/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest request)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && userIdFromToken != request.UserId.ToString())
            {
                return Forbid();
            }

            await _mediator.Send(request);
            return NoContent();
        }

        // DELETE: api/reviews/{id}
        [Authorize(Roles = "Admin,User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReview([FromBody] DeleteReviewRequest request)
        {
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && userIdFromToken != request.UserId.ToString())
            {
                return Forbid();
            }

            await _mediator.Send(request);
            return NoContent();
        }
    }
}
