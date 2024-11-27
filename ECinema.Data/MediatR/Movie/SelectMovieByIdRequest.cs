using ECinema.Data.Entities;
using MediatR;

namespace ECinema.Data.MediatR.Movie
{
    public class SelectMovieByIdRequest : IRequest<Entities.Movie>
    {
        public int MovieId { get; set; }
    }
}
