using MediatR;

namespace ECinema.Data.MediatR.Movie
{
    public class DeleteMovieByIdRequest : IRequest<bool>
    {
        public int MovieId { get; set; }
    }
}
