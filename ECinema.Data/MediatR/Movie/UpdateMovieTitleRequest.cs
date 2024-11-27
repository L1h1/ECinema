using MediatR;

namespace ECinema.Data.MediatR.Movie
{
    public class UpdateMovieTitleRequest : IRequest<bool>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
    }
}
