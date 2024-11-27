using MediatR;

namespace ECinema.Data.MediatR.Cinema
{
    public class DeleteCinemaByIdRequest : IRequest
    {
        public int CinemaId { get; set; }
    }
}
