using MediatR;

namespace ECinema.Data.MediatR.Cinema
{
    public class UpdateCinemaNameRequest : IRequest
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
    }
}
