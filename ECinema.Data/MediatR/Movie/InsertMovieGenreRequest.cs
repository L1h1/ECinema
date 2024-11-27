using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieGenre
{
    public class InsertMovieGenreRequest : IRequest<int>
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
    }
}
