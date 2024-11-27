using MediatR;

namespace ECinema.Data.MediatR.Cinema
{
    public class SelectCinemaByIdRequest : IRequest<Entities.Cinema>
    {
        public int CinemaId { get; set; }
    }
}
