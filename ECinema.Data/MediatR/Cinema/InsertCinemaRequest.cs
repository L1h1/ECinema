using MediatR;

namespace ECinema.Data.MediatR.Cinema
{
    public class InsertCinemaRequest : IRequest
    {
        public string CinemaName { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
