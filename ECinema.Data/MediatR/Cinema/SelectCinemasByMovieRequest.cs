using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class SelectCinemasByMovieRequest : IRequest<List<Entities.Cinema>>
    {
        public int MovieId { get; set; }
    }
}
