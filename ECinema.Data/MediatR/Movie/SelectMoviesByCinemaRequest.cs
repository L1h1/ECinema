using MediatR;
using System.Collections.Generic;

namespace ECinema.Data.MediatR.MovieCinema
{
    public class SelectMoviesByCinemaRequest : IRequest<List<Entities.Movie>> 
    {
        public int CinemaId { get; set; }
    }
}
