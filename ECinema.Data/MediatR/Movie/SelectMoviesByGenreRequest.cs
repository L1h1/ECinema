using MediatR;
using System.Collections.Generic;

namespace ECinema.Data.MediatR.Movie
{
    public class SelectMoviesByGenreRequest : IRequest<List<Entities.Movie>>
    {
        public int GenreId { get; set; }
    }
}
