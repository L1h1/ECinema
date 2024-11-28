using MediatR;

namespace ECinema.Data.MediatR.Movie
{
    public class UpdateMovieDetailsRequest : IRequest<bool>
    {
        public int MovieId { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }

        public string TrailerUrl { get; set; }
    }
}
