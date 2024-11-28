using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class SelectActorsByMovieRequest : IRequest<List<Entities.Actor>>
    {
        public int MovieId { get; set; }
    }
}
