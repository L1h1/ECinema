using MediatR;
using System.Collections.Generic;

namespace ECinema.Data.MediatR.MovieActor
{
    public class SelectMoviesByActorRequest : IRequest<List<Entities.Movie>> 
    {
        public int ActorId { get; set; }
    }
}
