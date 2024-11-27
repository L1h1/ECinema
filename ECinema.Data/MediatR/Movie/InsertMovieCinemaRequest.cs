using MediatR;

namespace ECinema.Data.MediatR.MovieCinema
{
    public class InsertMovieCinemaRequest : IRequest<int> 
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
    }
}
