using MediatR;

namespace ECinema.Data.MediatR.Cinema
{
    public class UpdateCinemaWebsiteRequest : IRequest
    {
        public int CinemaId { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
