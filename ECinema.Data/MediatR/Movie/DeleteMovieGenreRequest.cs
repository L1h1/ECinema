using MediatR;

namespace ECinema.Data.MediatR.MovieGenre
{
    public class DeleteMovieGenreRequest : IRequest
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}
