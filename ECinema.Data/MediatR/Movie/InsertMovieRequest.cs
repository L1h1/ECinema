using MediatR;

namespace ECinema.Data.MediatR.Movie
{
    public class InsertMovieRequest : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public string TrailerUrl { get; set; }
    }
}
