using MediatR;
using System.Collections.Generic;

namespace ECinema.Data.MediatR.Review
{
    public class SelectReviewsByMovieRequest : IRequest<List<Entities.Review>>
    {
        public int MovieId { get; set; }
    }
}
