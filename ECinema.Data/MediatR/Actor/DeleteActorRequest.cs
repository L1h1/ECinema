using MediatR;

namespace ECinema.Data.MediatR.Actor
{
    public class DeleteActorRequest : IRequest
    {
        public int ActorId { get; set; }
    }
}
