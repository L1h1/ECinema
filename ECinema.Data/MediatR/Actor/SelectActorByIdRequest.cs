using MediatR;
using ECinema.Data.Entities;

namespace ECinema.Data.MediatR.Actor
{
    public class SelectActorByIdRequest : IRequest<Entities.Actor>
    {
        public int ActorId { get; set; }
    }
}
